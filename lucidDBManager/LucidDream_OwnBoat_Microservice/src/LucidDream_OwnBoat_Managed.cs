////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//          Generated at : 31/10/2019 15:16:17 ,in TIK46593 PC ,by liran harari
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using LucidDreamContractManager_Managed;
using DDS_ManagedShell;
using LucidDream_DataTypesManaged;
using LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_nav_data;
using LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data;
using LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_system_target_data;
using LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data;
using Newtonsoft.Json;

namespace LucidDreamSystem
{
    class LucidDreamSystemClient
    {
        
        #region Data members
		// contarct file path
        static string contractPath = "../config/LucidDream_ContractManagerSystem_Contract.xml";

        //contract Manager
        private LucidDreamContractManager m_LucidDreamContractManager;

        #region DataWriters
        private LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeDataWriter m_OwnBoatWriter;
        #endregion

        #region DataReaders
        private LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeDataReader m_OwnBoatReader;
        #endregion
        #endregion

        #region Properties

        public RabbitMQSender rabbit;
        public LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeDataWriter OwnBoatWriter
        {
            get { return m_OwnBoatWriter; }
            set { m_OwnBoatWriter = value; }
        }

        public LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeDataReader OwnBoatReader
        {
            get { return m_OwnBoatReader; }
            set { m_OwnBoatReader = value; }
        }


        #endregion

        public void SetupDataWriters()
		{
            OwnBoatWriter = m_LucidDreamContractManager.Get_LucidDreamParticipant_OwnBoatWriter();
        }

        public void SetupDataReaders()
		{
            OwnBoatReader = m_LucidDreamContractManager.Get_LucidDreamParticipant_OwnBoatReader();
        }

        public void RegistrationToEvents()
		{


            #region Register OwnBoatWriter events
            OwnBoatWriter.OnLivelinessLost += new WriterLivelinessLostDelegate(OnLivelinessLostHendler);
            OwnBoatWriter.OnOfferedDeadlineMissed += new OfferedDeadlineMissedDelegate(OnOfferedDeadlineMissedHendler);
            OwnBoatWriter.OnOfferedIncompatibleQOS += new OfferedIncompatibleQOSDelegate(OnOfferedIncompatibleQOSHendler);
            OwnBoatWriter.OnPublicationMatched += new PublicationMatchedDelegate(OnPublicationMatchedHendler);
            #endregion

            #region Register OwnBoatReader events
            OwnBoatReader.OnSampleArrived += new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeSampleArrivedHandler(OwnBoatReaderOnSampleArrived);
            OwnBoatReader.OnInstanceNotAliveNoWriters += new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeInstanceNotAliveNoWritersHandler(OwnBoatReaderOnInstanceNotAliveNoWriters);
            OwnBoatReader.OnLivelinessGained += new DDS_ManagedShell.LivelinessGainedDelegate(OnLivelinessGainedEventHendler);
            OwnBoatReader.OnLivelinessLost += new ReaderLivelinessLostDelegate(OnLivelinessLostEventHendler);
            OwnBoatReader.OnRequestedDeadlineMissed += new RequestedDeadlineMissedDelegate(OnRequestedDeadlineMissedEventHendler);
            OwnBoatReader.OnRequestedIncompatibleQos += new DDS_ManagedShell.RequestedIncompatibleQosDelegate(OnRequestedIncompatibleQosEventHendler);
            OwnBoatReader.OnSampleLost += new DDS_ManagedShell.SampleLostDelegate(OnSampleLostEventHendler);
            OwnBoatReader.OnSampleRejected += new DDS_ManagedShell.SampleRejectedDelegate(OnSampleRejectedEventHendler);
            OwnBoatReader.OnSubscriptionMatched += new DDS_ManagedShell.SubscriptionMatchedDelegate(OnSubscriptionMatchedEventHendler);
            #endregion


        }

        public void Init()
        {
            m_LucidDreamContractManager = new LucidDreamContractManager();
            rabbit = new RabbitMQSender();
            
            m_LucidDreamContractManager.LoadFromFile(contractPath);
            m_LucidDreamContractManager.VivifyAll();
            SetupDataWriters();
            SetupDataReaders();
            RegistrationToEvents();
        }
        
        public void EnableAll()
        {
			m_LucidDreamContractManager.EnableAll();
        }
        
        public void Publish()
        {
            LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_type idl_idde_itfmod_to_3pa_own_boat_data_idde_itfmod_to_3pa_own_boat_data_type = new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_type();


            for (int i = 0; i < 500; i++)
			{
				System.Threading.Thread.Sleep(1000);

                //OwnBoatWriter.Write(idl_idde_itfmod_to_3pa_own_boat_data_idde_itfmod_to_3pa_own_boat_data_type);

                //Console.WriteLine("\n****** Samples set #" + i + " was sent ******\n");

                // To dispose some dataType use this code
                //mySystemClient.SomeDataWriter.Dispose(dataType);
            }
        }

        public void Shutdown()
        {


            #region Unregister OwnBoatWriter events
            OwnBoatWriter.OnLivelinessLost -= new WriterLivelinessLostDelegate(OnLivelinessLostHendler);
            OwnBoatWriter.OnOfferedDeadlineMissed -= new OfferedDeadlineMissedDelegate(OnOfferedDeadlineMissedHendler);
            OwnBoatWriter.OnOfferedIncompatibleQOS -= new OfferedIncompatibleQOSDelegate(OnOfferedIncompatibleQOSHendler);
            OwnBoatWriter.OnPublicationMatched -= new PublicationMatchedDelegate(OnPublicationMatchedHendler);
            #endregion

            #region Unregister OwnBoatReader events
            OwnBoatReader.OnSampleArrived -= new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeSampleArrivedHandler(OwnBoatReaderOnSampleArrived);
            OwnBoatReader.OnInstanceNotAliveNoWriters -= new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeInstanceNotAliveNoWritersHandler(OwnBoatReaderOnInstanceNotAliveNoWriters);
            OwnBoatReader.OnLivelinessGained -= new DDS_ManagedShell.LivelinessGainedDelegate(OnLivelinessGainedEventHendler);
            OwnBoatReader.OnLivelinessLost -= new ReaderLivelinessLostDelegate(OnLivelinessLostEventHendler);
            OwnBoatReader.OnRequestedDeadlineMissed -= new RequestedDeadlineMissedDelegate(OnRequestedDeadlineMissedEventHendler);
            OwnBoatReader.OnRequestedIncompatibleQos -= new DDS_ManagedShell.RequestedIncompatibleQosDelegate(OnRequestedIncompatibleQosEventHendler);
            OwnBoatReader.OnSampleLost -= new DDS_ManagedShell.SampleLostDelegate(OnSampleLostEventHendler);
            OwnBoatReader.OnSampleRejected -= new DDS_ManagedShell.SampleRejectedDelegate(OnSampleRejectedEventHendler);
            OwnBoatReader.OnSubscriptionMatched -= new DDS_ManagedShell.SubscriptionMatchedDelegate(OnSubscriptionMatchedEventHendler);
            #endregion

            m_LucidDreamContractManager.SubdueAll();
            m_LucidDreamContractManager.UnLoad();
        }
        
        #region Events
        #region General DataWriters Event
        /*! This event is raised when the DataWriter failed to write data
        within the time period set in its DeadlineQosPolicy */
        public void OnOfferedDeadlineMissedHendler(DataWriter dataWriter , OfferedDeadlineMissedStatus status)
        {
			Console.WriteLine("In DataWriter " + dataWriter.Name +
                              " missed its offered deadline " + status.TotalCountChanged + " times.");
        }

        /*! This event is raised when the DataWriter failed to signal its liveliness
        within the time specified by the LivelinessQosPolicy */
        public void OnLivelinessLostHendler(DataWriter dataWriter, LivelinessLostStatus status)
        {
            Console.WriteLine("Liveliness Lost on DataWriter: " + dataWriter.Name);
        }

        /*! This event is raised when the DataWriter discovered a DataReader for
        the same topic, but the DataReader had requested Qos settings incompatible 
        with this DataWriter's offered Qos */
        public void OnOfferedIncompatibleQOSHendler(DataWriter dataWriter,OfferedIncompatibleQOSStatus status)
        {
			Console.WriteLine("In DataWriter " + dataWriter.Name +
                             " QoS policies, that were incompatible with remote DataReader. Check contracts on both sides.");
        }
        
		/*! This event is raised when the DataWriter discovered a matching DataReader */
        public void OnPublicationMatchedHendler(DataWriter dataWriter, PublicationMatchedStatus status)
        {
			if (status.CurrentCountChange > 0)
            {
                Console.WriteLine("in DataWriter: " + dataWriter.Name + " " + status.CurrentCountChange + " Publication matched");
            }
            else
            {
                Console.WriteLine("in DataWriter: " + dataWriter.Name + " " + status.CurrentCountChange + " DataReader lost");
            }
        }
        #endregion

        #region General DataReaders Event
        /*! This event is raised when the DataReader did not receive
		a new sample for an data-instance within the time period 
		set in the DataReader's DeadlineQosPolicy */
        public void OnRequestedDeadlineMissedEventHendler(DataReader dataReader, RequestedDeadlineMissedStatus status)
        {
			Console.WriteLine("requested deadline missed on DataReader " + dataReader.Name +
                             "requested deadline that was not respected by DataWriter " +
                             status.TotalCountChanged);
        }

		/*! This event is raised when the number of matched DataWriters that are 
        currently alive changed from any number to 0 */
        public void OnLivelinessLostEventHendler(DataReader dataReader, LivelinessChangedStatus status)
        {
			Console.WriteLine("Liveliness lost of " + dataReader.Name);
        }
        
		/*! This event is raised when the number of matched DataWriters that are 
        currently alive increased from 0 to 1 */
        public void OnLivelinessGainedEventHendler(DataReader dataReader, LivelinessChangedStatus status)
        {
			Console.WriteLine("a new Liveliness gained of " + dataReader.Name);
        }

		/*! This event is raised when the DataReader discovered a dataWriter for 
        the same Topic, but that DataReader has requested Qos settings incompatible
        with this DataWriter's offered Qos */
        public void OnRequestedIncompatibleQosEventHendler(DataReader dataReader, RequestedIncompatibleQOSStatus status)
        {
			Console.WriteLine("Incompatible Qos on topic " + dataReader.Name +
			                   " QoS policies, that were inconsistent with DataWriter. Check contracts on both sides.");
        }

        /*! This event is raised when one or more samples received from the DataWriter
        have been dropped by the DataReader */
        public void OnSampleRejectedEventHendler(DataReader dataReader, SampleRejectedStatus status)
        {
			Console.WriteLine(" sample rejected in DataReader: " + dataReader.Name + "the reason is: " +
                                "samples were rejected. Usually this happens when DataReader's memory resources are exhausted.");
        }
        
		/*! This event is raised when one or more samples received from the DataWriter
        have failed to be received */
        public void OnSampleLostEventHendler(DataReader dataReader, SampleLostStatus status)
        {
			Console.WriteLine("\n" + status.TotalCountChanged + " Sample lost on DataReader: " + dataReader.Name +
			                  "\n until now " + status.TotalCount + " samples lost" + 
			                  "\n samples were lost. Usually this happens when DataWriter writes faster than DataReader reads.");

        }
        
		/*! This event is raised when the DataReader discovered a matching DataWriter */
        public void OnSubscriptionMatchedEventHendler(DataReader dataReader, SubscriptionMatchedStatus status)
        {
			if (status.CurrentCountChange > 0)
            {
                Console.WriteLine("in dataReader: " + dataReader.Name + " " + status.CurrentCountChange + " subscription matched");
            }
            else
            {
                Console.WriteLine("in dataReader: " + dataReader.Name + " " + status.CurrentCountChange + " an existing matched DataWriter has been deleted");
            }
        }
        #endregion

        #region Specific DataReaders Events
        #region OwnBoatReader Events
        public void OwnBoatReaderOnSampleArrived(LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeDataReader dr, LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_type dataType, SampleInfo info, ValidityStatus validity)
        {
            OwnBoat_OriginalMessage converted_data = ConvertData(dataType);
            string data = JsonConvert.SerializeObject(converted_data);
            rabbit.send_data(data);
            Console.WriteLine("a new sample of \"idde_itfmod_to_3pa_own_boat_data_type\" has arrived");
        }


        public void OwnBoatReaderOnInstanceNotAliveNoWriters(LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_typeDataReader dr, LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_own_boat_data.idde_itfmod_to_3pa_own_boat_data_type dataType, SampleInfo info)
        {
            Console.WriteLine("an instance of \"idde_itfmod_to_3pa_own_boat_data_type\" has lost all its writers");
        }

        #endregion
        #endregion
        #endregion

        public OwnBoat_OriginalMessage ConvertData(idde_itfmod_to_3pa_own_boat_data_type message)
        {
            OwnBoat_OriginalMessage newDataClass = new OwnBoat_OriginalMessage();

            newDataClass.idlHeader.message_state = message.idl_header.message_state;
            newDataClass.idlHeader.message_source = message.idl_header.message_source;
            newDataClass.idlHeader.number_of_bytes = message.idl_header.number_of_bytes;
            newDataClass.idlHeader.compile_time_of_message.time.hours = message.idl_header.compile_time_of_message.time.hours;
            newDataClass.idlHeader.compile_time_of_message.time.minutes = message.idl_header.compile_time_of_message.time.minutes;
            newDataClass.idlHeader.compile_time_of_message.time.seconds = message.idl_header.compile_time_of_message.time.seconds;
            newDataClass.idlHeader.compile_time_of_message.time.c_seconds = message.idl_header.compile_time_of_message.time.c_seconds;
            newDataClass.idlHeader.compile_time_of_message.date.year = message.idl_header.compile_time_of_message.date.year;
            newDataClass.idlHeader.compile_time_of_message.date.month = message.idl_header.compile_time_of_message.date.month;
            newDataClass.idlHeader.compile_time_of_message.date.day = message.idl_header.compile_time_of_message.date.day;

            newDataClass.systemTime.is_current = message.system_time.is_current;
            newDataClass.systemTime.sensor = message.system_time.sensor;
            newDataClass.systemTime.time.valid = message.system_time.time.valid;
            newDataClass.systemTime.time.value.time.hours = message.system_time.time.value.time.hours;
            newDataClass.systemTime.time.value.time.minutes = message.system_time.time.value.time.minutes;
            newDataClass.systemTime.time.value.time.seconds = message.system_time.time.value.time.seconds;
            newDataClass.systemTime.time.value.time.c_seconds = message.system_time.time.value.time.c_seconds;
            newDataClass.systemTime.time.value.date.year = message.system_time.time.value.date.year;
            newDataClass.systemTime.time.value.date.month = message.system_time.time.value.date.month;
            newDataClass.systemTime.time.value.date.day = message.system_time.time.value.date.day;

            newDataClass.timezone.data.valid = message.timezone.data.valid;
            newDataClass.timezone.data.value = message.timezone.data.value;
            newDataClass.timezone.is_current = message.timezone.is_current;
            newDataClass.timezone.sensor = message.timezone.sensor;
            newDataClass.timezone.time.valid = message.timezone.time.valid;
            newDataClass.timezone.time.value.time.hours = message.timezone.time.value.time.hours;
            newDataClass.timezone.time.value.time.minutes = message.timezone.time.value.time.minutes;
            newDataClass.timezone.time.value.time.seconds = message.timezone.time.value.time.seconds;
            newDataClass.timezone.time.value.time.c_seconds = message.timezone.time.value.time.c_seconds;
            newDataClass.timezone.time.value.date.year = message.timezone.time.value.date.year;
            newDataClass.timezone.time.value.date.month = message.timezone.time.value.date.month;
            newDataClass.timezone.time.value.date.day = message.timezone.time.value.date.day;

            newDataClass.heading.data.valid = message.heading.data.valid;
            newDataClass.heading.data.value = message.heading.data.value;
            newDataClass.heading.is_current = message.heading.is_current;
            newDataClass.heading.sensor = message.heading.sensor;
            newDataClass.heading.time.valid = message.heading.time.valid;
            newDataClass.heading.time.value.time.hours = message.heading.time.value.time.hours;
            newDataClass.heading.time.value.time.minutes = message.heading.time.value.time.minutes;
            newDataClass.heading.time.value.time.seconds = message.heading.time.value.time.seconds;
            newDataClass.heading.time.value.time.c_seconds = message.heading.time.value.time.c_seconds;
            newDataClass.heading.time.value.date.year = message.heading.time.value.date.year;
            newDataClass.heading.time.value.date.month = message.heading.time.value.date.month;
            newDataClass.heading.time.value.date.day = message.heading.time.value.date.day;

            newDataClass.heading_rate.data.valid = message.heading_rate.data.valid;
            newDataClass.heading_rate.data.value = message.heading_rate.data.value;
            newDataClass.heading_rate.is_current = message.heading_rate.is_current;
            newDataClass.heading_rate.sensor = message.heading_rate.sensor;
            newDataClass.heading_rate.time.valid = message.heading_rate.time.valid;
            newDataClass.heading_rate.time.value.time.hours = message.heading_rate.time.value.time.hours;
            newDataClass.heading_rate.time.value.time.minutes = message.heading_rate.time.value.time.minutes;
            newDataClass.heading_rate.time.value.time.seconds = message.heading_rate.time.value.time.seconds;
            newDataClass.heading_rate.time.value.time.c_seconds = message.heading_rate.time.value.time.c_seconds;
            newDataClass.heading_rate.time.value.date.year = message.heading_rate.time.value.date.year;
            newDataClass.heading_rate.time.value.date.month = message.heading_rate.time.value.date.month;
            newDataClass.heading_rate.time.value.date.day = message.heading_rate.time.value.date.day;

            newDataClass.roll.data.valid = message.roll.data.valid;
            newDataClass.roll.data.value = message.roll.data.value;
            newDataClass.roll.is_current = message.roll.is_current;
            newDataClass.roll.sensor = message.roll.sensor;
            newDataClass.roll.time.valid = message.roll.time.valid;
            newDataClass.roll.time.value.time.hours = message.roll.time.value.time.hours;
            newDataClass.roll.time.value.time.minutes = message.roll.time.value.time.minutes;
            newDataClass.roll.time.value.time.seconds = message.roll.time.value.time.seconds;
            newDataClass.roll.time.value.time.c_seconds = message.roll.time.value.time.c_seconds;
            newDataClass.roll.time.value.date.year = message.roll.time.value.date.year;
            newDataClass.roll.time.value.date.month = message.roll.time.value.date.month;
            newDataClass.roll.time.value.date.day = message.roll.time.value.date.day;

            newDataClass.roll_rate.data.valid = message.roll_rate.data.valid;
            newDataClass.roll_rate.data.value = message.roll_rate.data.value;
            newDataClass.roll_rate.is_current = message.roll_rate.is_current;
            newDataClass.roll_rate.sensor = message.roll_rate.sensor;
            newDataClass.roll_rate.time.valid = message.roll_rate.time.valid;
            newDataClass.roll_rate.time.value.time.hours = message.roll_rate.time.value.time.hours;
            newDataClass.roll_rate.time.value.time.minutes = message.roll_rate.time.value.time.minutes;
            newDataClass.roll_rate.time.value.time.seconds = message.roll_rate.time.value.time.seconds;
            newDataClass.roll_rate.time.value.time.c_seconds = message.roll_rate.time.value.time.c_seconds;
            newDataClass.roll_rate.time.value.date.year = message.roll_rate.time.value.date.year;
            newDataClass.roll_rate.time.value.date.month = message.roll_rate.time.value.date.month;
            newDataClass.roll_rate.time.value.date.day = message.roll_rate.time.value.date.day;

            newDataClass.pitch.data.valid = message.pitch.data.valid;
            newDataClass.pitch.data.value = message.pitch.data.value;
            newDataClass.pitch.is_current = message.pitch.is_current;
            newDataClass.pitch.sensor = message.pitch.sensor;
            newDataClass.pitch.time.valid = message.pitch.time.valid;
            newDataClass.pitch.time.value.time.hours = message.pitch.time.value.time.hours;
            newDataClass.pitch.time.value.time.minutes = message.pitch.time.value.time.minutes;
            newDataClass.pitch.time.value.time.seconds = message.pitch.time.value.time.seconds;
            newDataClass.pitch.time.value.time.c_seconds = message.pitch.time.value.time.c_seconds;
            newDataClass.pitch.time.value.date.year = message.pitch.time.value.date.year;
            newDataClass.pitch.time.value.date.month = message.pitch.time.value.date.month;
            newDataClass.pitch.time.value.date.day = message.pitch.time.value.date.day;

            newDataClass.pitch_rate.data.valid = message.pitch_rate.data.valid;
            newDataClass.pitch_rate.data.value = message.pitch_rate.data.value;
            newDataClass.pitch_rate.is_current = message.pitch_rate.is_current;
            newDataClass.pitch_rate.sensor = message.pitch_rate.sensor;
            newDataClass.pitch_rate.time.valid = message.pitch_rate.time.valid;
            newDataClass.pitch_rate.time.value.time.hours = message.pitch_rate.time.value.time.hours;
            newDataClass.pitch_rate.time.value.time.minutes = message.pitch_rate.time.value.time.minutes;
            newDataClass.pitch_rate.time.value.time.seconds = message.pitch_rate.time.value.time.seconds;
            newDataClass.pitch_rate.time.value.time.c_seconds = message.pitch_rate.time.value.time.c_seconds;
            newDataClass.pitch_rate.time.value.date.year = message.pitch_rate.time.value.date.year;
            newDataClass.pitch_rate.time.value.date.month = message.pitch_rate.time.value.date.month;
            newDataClass.pitch_rate.time.value.date.day = message.pitch_rate.time.value.date.day;

            newDataClass.heave.data.valid = message.heave.data.valid;
            newDataClass.heave.data.value = message.heave.data.value;
            newDataClass.heave.is_current = message.heave.is_current;
            newDataClass.heave.sensor = message.heave.sensor;
            newDataClass.heave.time.valid = message.heave.time.valid;
            newDataClass.heave.time.value.time.hours = message.heave.time.value.time.hours;
            newDataClass.heave.time.value.time.minutes = message.heave.time.value.time.minutes;
            newDataClass.heave.time.value.time.seconds = message.heave.time.value.time.seconds;
            newDataClass.heave.time.value.time.c_seconds = message.heave.time.value.time.c_seconds;
            newDataClass.heave.time.value.date.year = message.heave.time.value.date.year;
            newDataClass.heave.time.value.date.month = message.heave.time.value.date.month;
            newDataClass.heave.time.value.date.day = message.heave.time.value.date.day;

            newDataClass.heave.data.valid = message.heave.data.valid;
            newDataClass.heave.data.value = message.heave.data.value;
            newDataClass.heave.is_current = message.heave.is_current;
            newDataClass.heave.sensor = message.heave.sensor;
            newDataClass.heave.time.valid = message.heave.time.valid;
            newDataClass.heave.time.value.time.hours = message.heave.time.value.time.hours;
            newDataClass.heave.time.value.time.minutes = message.heave.time.value.time.minutes;
            newDataClass.heave.time.value.time.seconds = message.heave.time.value.time.seconds;
            newDataClass.heave.time.value.time.c_seconds = message.heave.time.value.time.c_seconds;
            newDataClass.heave.time.value.date.year = message.heave.time.value.date.year;
            newDataClass.heave.time.value.date.month = message.heave.time.value.date.month;
            newDataClass.heave.time.value.date.day = message.heave.time.value.date.day;

            newDataClass.heave_rate.data.valid = message.heave_rate.data.valid;
            newDataClass.heave_rate.data.value = message.heave_rate.data.value;
            newDataClass.heave_rate.is_current = message.heave_rate.is_current;
            newDataClass.heave_rate.sensor = message.heave_rate.sensor;
            newDataClass.heave_rate.time.valid = message.heave_rate.time.valid;
            newDataClass.heave_rate.time.value.time.hours = message.heave_rate.time.value.time.hours;
            newDataClass.heave_rate.time.value.time.minutes = message.heave_rate.time.value.time.minutes;
            newDataClass.heave_rate.time.value.time.seconds = message.heave_rate.time.value.time.seconds;
            newDataClass.heave_rate.time.value.time.c_seconds = message.heave_rate.time.value.time.c_seconds;
            newDataClass.heave_rate.time.value.date.year = message.heave_rate.time.value.date.year;
            newDataClass.heave_rate.time.value.date.month = message.heave_rate.time.value.date.month;
            newDataClass.heave_rate.time.value.date.day = message.heave_rate.time.value.date.day;

            newDataClass.course_over_ground.data.valid = message.course_over_ground.data.valid;
            newDataClass.course_over_ground.data.value = message.course_over_ground.data.value;
            newDataClass.course_over_ground.is_current = message.course_over_ground.is_current;
            newDataClass.course_over_ground.sensor = message.course_over_ground.sensor;
            newDataClass.course_over_ground.time.valid = message.course_over_ground.time.valid;
            newDataClass.course_over_ground.time.value.time.hours = message.course_over_ground.time.value.time.hours;
            newDataClass.course_over_ground.time.value.time.minutes = message.course_over_ground.time.value.time.minutes;
            newDataClass.course_over_ground.time.value.time.seconds = message.course_over_ground.time.value.time.seconds;
            newDataClass.course_over_ground.time.value.time.c_seconds = message.course_over_ground.time.value.time.c_seconds;
            newDataClass.course_over_ground.time.value.date.year = message.course_over_ground.time.value.date.year;
            newDataClass.course_over_ground.time.value.date.month = message.course_over_ground.time.value.date.month;
            newDataClass.course_over_ground.time.value.date.day = message.course_over_ground.time.value.date.day;


            return newDataClass;

        }
    }


    class SubscriberProgram
    {
        static void Main(string[] args)
        {
            LucidDreamSystemClient mySystemClient = new LucidDreamSystemClient();
            mySystemClient.Init();
			mySystemClient.EnableAll();
			mySystemClient.Publish();
            
            Console.WriteLine("Shutting Down...");
            mySystemClient.Shutdown();
        }
    }
}
