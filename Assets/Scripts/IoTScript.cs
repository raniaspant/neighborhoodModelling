using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IoTScript : MonoBehaviour {
    public string MACAddress; //how objects are identified on any wireless network
    public string Description; //Phone, FitBit, etc. in case you want to display a description to the user
    public double SignalRange = 5;
    public Sensation MySensorCapabilities;
    public Sensation MyAlertCapabilities;

    [Flags]
    public enum Sensation : int
    {
        None = 0x0,
        Wind = 0x1,
        Temperature = 0x2,
        Audio = 0x4,
        Light = 0x8,
        Vibration = 0x10
    }

    public class Emergency
    {
        public static readonly Emergency[] AllEmergencies = { Fire, Gunfire, Tornado };

        public static readonly Emergency Fire = new Emergency
        {
            Description = "Fire",
            Max = 10000,
            Min = 475,
            Measure = Sensation.Temperature
        }; //Paper burns at 491~519 Kelvin: http://www.tcforensic.com.au/docs/article10.html

        public static readonly Emergency Tornado = new Emergency
        {
            Description = "Tornado",
            Max = 10000,
            Min = 50,
            Measure = Sensation.Wind
        }; //Danger scales with speed, but 50 MPH seems like a safe starting point: http://www.tornadoproject.com/cellar/fscale.htm

        public static readonly Emergency Gunfire = new Emergency
        {
            Description = "Gunfire",
            Max = 180,
            Min = 100,
            Measure = Sensation.Wind
        }; //Volume of gunfire depends on distance from the source, but small arms fire within ~300 meters sounds like a good start: https://www.ncbi.nlm.nih.gov/pubmed/7761796

        //Could be others: panic (generally the result of an emergency), heart attack, and other health conditions could be detected by an IoT device with a heart rate monitor.

        public Sensation Measure;
        public double Min;
        public double Max;
        public string Description;
    }

    public class Broadcast {
        public string Source; //MAC address
        public Sensation DataType; //such as Sensor.Wind or Sensor.Temperature (it's a flags enum)
        public double Payload; //such as wind speed or temperature in Kelvins
    }

    public List<Broadcast> ActiveBroadcasts = new List<Broadcast>(); //TODO: List my active broadcasts so others' colliders can pick up what I'm droppin'

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        List<Broadcast> incomingBroadcasts = new List<Broadcast>(); //TODO: Get all broadcasts from the collider
        ActiveBroadcasts.Clear(); //Forget what I was broadcasting before

        //TODO: Receive broadcasts, if any
        //TODO: First, need a collider
        //ActiveBroadcasts.AddRange(incomingBroadcasts);

        //Loop through all incoming broadcasts
        //If the source is empty string, you need a sensor to rebroadcast
        foreach (var bc in incomingBroadcasts)
        {
            if (bc.Source == String.Empty)
            {
                //This is an analog signal, such as an actual fire. We need a sensor to be able to receive this signal.
                if ((bc.DataType & MySensorCapabilities) != Sensation.None)
                {
                    bc.Source = MACAddress; //When this IoT device digitally broadcasts this analog signal, it will have its own MAC address as the source.
                    //Now check if the signal is an emergency (if it's worth retransmitting)
                    if (Emergency.AllEmergencies.Any(p => p.Measure == bc.DataType && p.Min <= bc.Payload && p.Max >= bc.Payload))
                    {
                        ActiveBroadcasts.Add(bc); //We're retransmitting it digitally with our source.
                        //Also retransmit it in analog based on whatever capabilties we have.
                        foreach (Sensation analogBroadcast in Enum.GetValues(typeof(Sensation)))
                        {
                            if ((analogBroadcast & MyAlertCapabilities) != Sensation.None) //If I can analogue signal in this way, I shall do so. Use an empty string as the source to show it's not a digital transmission.
                            {
                                ActiveBroadcasts.Add(new Broadcast { DataType = analogBroadcast, Payload = bc.Payload, Source = String.Empty });
                            }
                        }
                    }
                }
            }
            else
            {
                //Digital signal (a wireless broadcast)
                ActiveBroadcasts.Add(bc); //Note: broadcasts will be transmitted infinitely once they begin.
            }
        }


        //Use my sensors, if any
        foreach (Sensation sensor in Enum.GetValues(typeof(Sensation)))
        {
            if ((MySensorCapabilities & sensor) != Sensation.None) //If I have this type of sensor
            {
                Broadcast potentialBroadcast = new Broadcast() { DataType = sensor, Source = MACAddress };

                //Receive a signal (emergency) from a non-IoT source object, which emits a Broadcast with a Source of empty string
                foreach (var bc in incomingBroadcasts)
                {
                    potentialBroadcast.Payload = bc.Payload;

                    //If the data matches a potential broadcast
                    if (Emergency.AllEmergencies.Any(p => p.Measure == potentialBroadcast.DataType && p.Min <= potentialBroadcast.Payload && p.Max >= potentialBroadcast.Payload))
                    {
                        //If I'm not already re-broadcasting someone's signal about the same type of emergency I just detected, then add a new broadcast about an emergency I detected
                        if (!ActiveBroadcasts.Any(p => p.DataType == potentialBroadcast.DataType)) ActiveBroadcasts.Add(potentialBroadcast);
                    }
                }
            }
        }
        //TODO: Need a signal (emergency) source object to emit wind, temperature, or audio values that I can detect.

        //TODO: Broadcast the ActiveBroadcasts list to other IoT devices and humans via a collider
    }
}
