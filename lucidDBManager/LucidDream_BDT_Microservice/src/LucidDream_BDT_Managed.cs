////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//          Generated at : 31/10/2019 15:16:17 ,in TIK46593 PC ,by liran harari
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using LucidDreamContractManager_Managed;
using DDS_ManagedShell;
using LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data;
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
        private LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeDataWriter m_BdtCasDataWriter;
        #endregion

        #region DataReaders
        private LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeDataReader m_BdtCasDataReader;
        #endregion
        #endregion

        #region Properties

        public RabbitMQSender rabbit;
        public LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeDataWriter BdtCasDataWriter
        {
            get { return m_BdtCasDataWriter; }
            set { m_BdtCasDataWriter = value; }
        }

        public LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeDataReader BdtCasDataReader
        {
            get { return m_BdtCasDataReader; }
            set { m_BdtCasDataReader = value; }
        }


        #endregion

        public void SetupDataWriters()
		{
            BdtCasDataWriter = m_LucidDreamContractManager.Get_LucidDreamParticipant_BdtCasDataWriter();
        }

        public void SetupDataReaders()
		{
            BdtCasDataReader = m_LucidDreamContractManager.Get_LucidDreamParticipant_BdtCasDataReader();
        }

        public void RegistrationToEvents()
		{


            #region Register BdtCasDataWriter events
            BdtCasDataWriter.OnLivelinessLost += new WriterLivelinessLostDelegate(OnLivelinessLostHendler);
            BdtCasDataWriter.OnOfferedDeadlineMissed += new OfferedDeadlineMissedDelegate(OnOfferedDeadlineMissedHendler);
            BdtCasDataWriter.OnOfferedIncompatibleQOS += new OfferedIncompatibleQOSDelegate(OnOfferedIncompatibleQOSHendler);
            BdtCasDataWriter.OnPublicationMatched += new PublicationMatchedDelegate(OnPublicationMatchedHendler);
            #endregion

            #region Register BdtCasDataReader events
            BdtCasDataReader.OnSampleArrived += new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeSampleArrivedHandler(BdtCasDataReaderOnSampleArrived);
            BdtCasDataReader.OnInstanceNotAliveNoWriters += new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeInstanceNotAliveNoWritersHandler(BdtCasDataReaderOnInstanceNotAliveNoWriters);
            BdtCasDataReader.OnLivelinessGained += new DDS_ManagedShell.LivelinessGainedDelegate(OnLivelinessGainedEventHendler);
            BdtCasDataReader.OnLivelinessLost += new ReaderLivelinessLostDelegate(OnLivelinessLostEventHendler);
            BdtCasDataReader.OnRequestedDeadlineMissed += new RequestedDeadlineMissedDelegate(OnRequestedDeadlineMissedEventHendler);
            BdtCasDataReader.OnRequestedIncompatibleQos += new DDS_ManagedShell.RequestedIncompatibleQosDelegate(OnRequestedIncompatibleQosEventHendler);
            BdtCasDataReader.OnSampleLost += new DDS_ManagedShell.SampleLostDelegate(OnSampleLostEventHendler);
            BdtCasDataReader.OnSampleRejected += new DDS_ManagedShell.SampleRejectedDelegate(OnSampleRejectedEventHendler);
            BdtCasDataReader.OnSubscriptionMatched += new DDS_ManagedShell.SubscriptionMatchedDelegate(OnSubscriptionMatchedEventHendler);
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
            LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_type idl_idde_itfmod_to_3pa_bdt_track_data_idde_itfmod_to_3pa_bdt_track_data_type = new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_type();


            while(true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        public void Shutdown()
        {


            #region Unregister BdtCasDataWriter events
            BdtCasDataWriter.OnLivelinessLost -= new WriterLivelinessLostDelegate(OnLivelinessLostHendler);
            BdtCasDataWriter.OnOfferedDeadlineMissed -= new OfferedDeadlineMissedDelegate(OnOfferedDeadlineMissedHendler);
            BdtCasDataWriter.OnOfferedIncompatibleQOS -= new OfferedIncompatibleQOSDelegate(OnOfferedIncompatibleQOSHendler);
            BdtCasDataWriter.OnPublicationMatched -= new PublicationMatchedDelegate(OnPublicationMatchedHendler);
            #endregion

            #region Unregister BdtCasDataReader events
            BdtCasDataReader.OnSampleArrived -= new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeSampleArrivedHandler(BdtCasDataReaderOnSampleArrived);
            BdtCasDataReader.OnInstanceNotAliveNoWriters -= new LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeInstanceNotAliveNoWritersHandler(BdtCasDataReaderOnInstanceNotAliveNoWriters);
            BdtCasDataReader.OnLivelinessGained -= new DDS_ManagedShell.LivelinessGainedDelegate(OnLivelinessGainedEventHendler);
            BdtCasDataReader.OnLivelinessLost -= new ReaderLivelinessLostDelegate(OnLivelinessLostEventHendler);
            BdtCasDataReader.OnRequestedDeadlineMissed -= new RequestedDeadlineMissedDelegate(OnRequestedDeadlineMissedEventHendler);
            BdtCasDataReader.OnRequestedIncompatibleQos -= new DDS_ManagedShell.RequestedIncompatibleQosDelegate(OnRequestedIncompatibleQosEventHendler);
            BdtCasDataReader.OnSampleLost -= new DDS_ManagedShell.SampleLostDelegate(OnSampleLostEventHendler);
            BdtCasDataReader.OnSampleRejected -= new DDS_ManagedShell.SampleRejectedDelegate(OnSampleRejectedEventHendler);
            BdtCasDataReader.OnSubscriptionMatched -= new DDS_ManagedShell.SubscriptionMatchedDelegate(OnSubscriptionMatchedEventHendler);
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
        #region BdtCasDataReader Events
        public void BdtCasDataReaderOnSampleArrived(LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeDataReader dr, LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_type dataType, SampleInfo info, ValidityStatus validity)
        {

            BDT_CAS_OriginalMessage converted_data = ConvertData(dataType);
            string data = JsonConvert.SerializeObject(converted_data);
            rabbit.SendData(data);
            Console.WriteLine("a new sample of \"idde_itfmod_to_3pa_bdt_track_data_type\" has arrived");
        }


        public void BdtCasDataReaderOnInstanceNotAliveNoWriters(LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_typeDataReader dr, LucidDream_DataTypesManaged.idl_idde_itfmod_to_3pa_bdt_track_data.idde_itfmod_to_3pa_bdt_track_data_type dataType, SampleInfo info)
        {
            Console.WriteLine("an instance of \"idde_itfmod_to_3pa_bdt_track_data_type\" has lost all its writers");
        }

        #endregion
        #endregion
        #endregion

        public BDT_CAS_OriginalMessage ConvertData(idde_itfmod_to_3pa_bdt_track_data_type message)
        {
            BDT_CAS_OriginalMessage newDataClass = new BDT_CAS_OriginalMessage();

            newDataClass.timeStamp.time.hours = message.time_reference.time.hours;
            newDataClass.timeStamp.time.minutes = message.time_reference.time.minutes;
            newDataClass.timeStamp.time.seconds = message.time_reference.time.seconds;
            newDataClass.timeStamp.time.c_seconds = message.time_reference.time.c_seconds;

            newDataClass.timeStamp.date.day = message.time_reference.date.day;
            newDataClass.timeStamp.date.month = message.time_reference.date.month;
            newDataClass.timeStamp.date.year = message.time_reference.date.year;

            for (int i = 0; i < message.track_data.Length; i++)
            {
                OriginalSystemTrack currTrack = new OriginalSystemTrack();
                currTrack.approach_receed_indicator = message.track_data.get_Item((uint)i).approach_receed_indicator;
                currTrack.bandwidth.valid = message.track_data.get_Item((uint)i).bandwidth.valid;
                currTrack.bandwidth.upper = message.track_data.get_Item((uint)i).bandwidth.upper;
                currTrack.bandwidth.lower = message.track_data.get_Item((uint)i).bandwidth.lower;
                currTrack.bearing = message.track_data.get_Item((uint)i).bearing;
                currTrack.bearingRate.valid = message.track_data.get_Item((uint)i).bearing_rate.valid;
                currTrack.bearingRate.value = message.track_data.get_Item((uint)i).bearing_rate.value;
                currTrack.constant_bearing_warning = message.track_data.get_Item((uint)i).constant_bearing_warning;
                currTrack.integration_time = message.track_data.get_Item((uint)i).integration_time;
                currTrack.integration_time_nominal = message.track_data.get_Item((uint)i).integration_time_nominal;
                currTrack.integrat_time_selection_mode = message.track_data.get_Item((uint)i).integrat_time_selection_mode;

                for (int j = 0; j < message.track_data.get_Item((uint)i).raw_bearing_candidates.Length; j++)
                {
                    currTrack.rawBearingCndidates.Add(new AngleValidType());
                    currTrack.rawBearingCndidates[j].valid = message.track_data.get_Item((uint)i).raw_bearing_candidates.get_Item((uint)j).valid;
                    currTrack.rawBearingCndidates[j].value = message.track_data.get_Item((uint)i).raw_bearing_candidates.get_Item((uint)j).value;
                }

                currTrack.state = message.track_data.get_Item((uint)i).state;
                currTrack.s_n_ratio = message.track_data.get_Item((uint)i).s_n_ratio;
                currTrack.target_level = message.track_data.get_Item((uint)i).target_level;

                currTrack.timeStamp.time.hours = message.track_data.get_Item((uint)i).time_reference.time.hours;
                currTrack.timeStamp.time.minutes = message.track_data.get_Item((uint)i).time_reference.time.minutes;
                currTrack.timeStamp.time.seconds = message.track_data.get_Item((uint)i).time_reference.time.seconds;
                currTrack.timeStamp.time.c_seconds = message.track_data.get_Item((uint)i).time_reference.time.c_seconds;

                currTrack.timeStamp.date.day = message.track_data.get_Item((uint)i).time_reference.date.day;
                currTrack.timeStamp.date.month = message.track_data.get_Item((uint)i).time_reference.date.month;
                currTrack.timeStamp.date.year = message.track_data.get_Item((uint)i).time_reference.date.year;

                currTrack.trackId = message.track_data.get_Item((uint)i).track_id;

                newDataClass.systemTracks.Add(currTrack);
            }

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
