using BdtCasMessage;
using NavMessage;
using BeamBusCas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MainSendWindow.Properties;
using System.Threading;
using WaterfallSimulator;

namespace MainSendWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavRabbitMQ _navRabbitMQ;
        private BdtCasRabbitMQ _bdtCasRabbitMQ;
        private Manager manager;

        private bool _isSendingUAGMessages;
        private bool _isSendingBeamBusCasMessages;

        public MainWindow()
        {
            InitializeComponent();
            _navRabbitMQ = new NavRabbitMQ(Settings.Default.Nav_ExchangeName);
            _bdtCasRabbitMQ = new BdtCasRabbitMQ(Settings.Default.BdtCas_ExchangeName);
            _isSendingUAGMessages = false;
            manager = new Manager();
        }

        private void sendBeamBusCasMessages_Click(object sender, RoutedEventArgs e)
        {
            if (!_isSendingBeamBusCasMessages)
            {
                _isSendingBeamBusCasMessages = true;
                BeamBusCasSender.isSending = true;
                beamBusCasButton.Content = "Stop Sending BeamBus Cas Messages";
                Thread beamBusCasSenderThread = new Thread(delegate ()
                            {
                                BeamBusCasSender.SendMessage();
                            });

                beamBusCasSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
                beamBusCasSenderThread.Start();
            }
            else
            {
                _isSendingBeamBusCasMessages = false;
                BeamBusCasSender.isSending = false;
                beamBusCasButton.Content = "Start Sending BeamBus Cas Messages";
            }

        }

        private void sendNavMessages_Click(object sender, RoutedEventArgs e)
        {

            OriginalNavMessage navObject = new OriginalNavMessage();
            Thread navSenderThread = new Thread(delegate ()
            {
                while (true)
                {
                    _navRabbitMQ.SendMessage(navObject);
                    Thread.Sleep(1000);
                }
            });
            navSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            navSenderThread.Start();

        }

        private void sendAllMessages_Click(object sender, RoutedEventArgs e)
        {
            if (!_isSendingUAGMessages)
            {
                _isSendingUAGMessages = true;
                allUAGButton.Content = "Stop Sending All UAG Messages";
                OriginalBdtCasMessage bdtCasObject = new OriginalBdtCasMessage();
                OriginalNavMessage navObject = new OriginalNavMessage();
                Thread allUAGSenderThread = new Thread(delegate ()
                {
                    while (_isSendingUAGMessages)
                    {
                        _bdtCasRabbitMQ.SendMessage(bdtCasObject);
                        _navRabbitMQ.SendMessage(navObject);
                        Thread.Sleep(1000);
                        Console.WriteLine("Send Nav And BdtCas Message");
                    }
                });
                allUAGSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
                allUAGSenderThread.Start();
            }
            else
            {
                _isSendingUAGMessages = false;
                allUAGButton.Content = "Start Sending All UAG Messages";
                Console.WriteLine("Stop Send Messages");
            }


        }

        private void sendBdtCasMessages_Click(object sender, RoutedEventArgs e)
        {
            OriginalBdtCasMessage bdtCasObject = new OriginalBdtCasMessage();
            Thread bdtCasSenderThread = new Thread(delegate ()
            {
                while (true)
                {
                    _bdtCasRabbitMQ.SendMessage(bdtCasObject);
                    Thread.Sleep(1000);
                }
            });

            bdtCasSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            bdtCasSenderThread.Start();
        }

        private void sendWaterFall_Click(object sender, RoutedEventArgs e)
        {
            Thread waterFallSenderThread = new Thread(delegate ()
            {
                manager.Start();
            });
            waterFallSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            waterFallSenderThread.Start();
        }
    }
}
