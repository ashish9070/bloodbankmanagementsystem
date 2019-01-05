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
using System.Windows.Shapes;


using BMS.PresentationLayerWpf.ChildWindows;
using BMS.PresentationLayerWpf.UISubWindows;
using System.Collections;
using BMS.Entities;
using BMS.BusinessLayer;
using BMS.PresentationLayerWpf.Utility;

namespace BMS.PresentationLayerWpf.ChildWindows
{
    /// <summary>
    /// Interaction logic for WindowChildren.xaml
    /// </summary>
    public partial class WindowChildren : Window
    {
        public WindowChildren()
        {
            InitializeComponent();
            
        }

        public void LoadWindow_StartupTab(IBAL<BmsBloodBank> balBank, IBAL<BmsBloodDonationCamp> balCamp, IBAL<BmsBloodDonor> balDonor,
            IBAL<BmsBloodDonorDonation> balDonation, IBAL<BmsBloodInventory> balInventory, IBAL<BmsHospital> balHospital,
             IBAL<BmsTransaction> balTransaction, TextBlock applicationStatus)
        {
            BMS.PresentationLayerWpf.UISubWindows.WindowStartup cc = 
                new BMS.PresentationLayerWpf.UISubWindows.WindowStartup();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(balBank, balCamp, balDonor, balDonation, balInventory, balHospital, balTransaction, applicationStatus);
        }

        public void LoadWindow_PageNotFound()
        {
           
                WindowPageNotFound cc = new WindowPageNotFound();
                ScrollViewer sv = cc.scrollview;


                cc.childPanel.Children.Remove(cc.scrollview);
                childWindowHolder.Children.Add(sv);
           
        }



        public void AttachData_Bank(IBAL<BmsBloodBank> bal, TextBlock applicationStatus, object values)
        {
            WindowDataViewer_BloodBank cc = new WindowDataViewer_BloodBank();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.AttachData(bal, applicationStatus, values);
        }


        public void AttachData_Camp(IBAL<BmsBloodDonationCamp> bal, TextBlock applicationStatus, object values)
        {
            WindowDataViewer_Events cc = new WindowDataViewer_Events();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.AttachData(bal, applicationStatus, values);
        }

        public void AttachData_Donor(IBAL<BmsBloodDonor> bal, TextBlock applicationStatus, object values)
        {
            WindowDataViewer_Donor cc = new WindowDataViewer_Donor();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.AttachData(bal, applicationStatus, values);
        }

        public void AttachData_Donation(IBAL<BmsBloodDonorDonation> bal, TextBlock applicationStatus, object values)
        {
            WindowDataViewer_Donation cc = new WindowDataViewer_Donation();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.AttachData(bal, applicationStatus, values);
        }

        public void AttachData_Inventory(IBAL<BmsBloodInventory> bal, TextBlock applicationStatus, object values)
        {
            WindowDataViewer_Inventory cc = new WindowDataViewer_Inventory();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.AttachData(bal, applicationStatus, values);
        }

        public void AttachData_Hospital(IBAL<BmsHospital> bal, TextBlock applicationStatus, object values)
        {
            WindowDataViewer_Hospital cc = new WindowDataViewer_Hospital();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.AttachData(bal, applicationStatus, values);
        }

        public void AttachData_Transaction(IBAL<BmsTransaction> bal, TextBlock applicationStatus, object values)
        {
            WindowDataViewer_Transaction cc = new WindowDataViewer_Transaction();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.AttachData(bal, applicationStatus, values);
        }
        /////////////////////


        public void ManageData_Bank(IBAL<BmsBloodBank> bal, TextBlock applicationStatus, ActionMode mode, object value = null)
        {
            WindowManage_BloodBank cc = new WindowManage_BloodBank();
            ScrollViewer sv = cc.scrollview;

            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(bal, applicationStatus, mode, value);
        }

        public void ManageData_Camp(IBAL<BmsBloodDonationCamp> bal, TextBlock applicationStatus, ActionMode mode, object value = null)
        {
            WindowManage_Events cc = new WindowManage_Events();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(bal, applicationStatus, mode, value);
        }

        public void ManageData_Donor(IBAL<BmsBloodDonor> bal, TextBlock applicationStatus, ActionMode mode, object value = null)
        {
            WindowManage_Donor cc = new WindowManage_Donor();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(bal, applicationStatus, mode, value);
        }

        public void ManageData_Donation(IBAL<BmsBloodDonorDonation> bal, TextBlock applicationStatus, ActionMode mode, object value = null)
        {
            WindowManage_Donation cc = new WindowManage_Donation();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(bal, applicationStatus, mode, value);
        }

        public void ManageData_Inventory(IBAL<BmsBloodInventory> bal, TextBlock applicationStatus, ActionMode mode, object value = null)
        {
            WindowManage_Inventory cc = new WindowManage_Inventory();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(bal, applicationStatus, mode, value);
        }

        public void ManageData_Hospital(IBAL<BmsHospital> bal, TextBlock applicationStatus, ActionMode mode, object value = null)
        {
            WindowManage_Hospital cc = new WindowManage_Hospital();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(bal, applicationStatus, mode, value);
        }

        public void ManageData_Transaction(IBAL<BmsTransaction> balTransaction, IBAL<BmsHospital> balHospital,
            IBAL<BmsBloodInventory> balInventory, TextBlock applicationStatus, ActionMode mode, object value = null)
        {
            WindowManage_Transaction cc = new WindowManage_Transaction();
            ScrollViewer sv = cc.scrollview;


            cc.childPanel.Children.Remove(cc.scrollview);
            childWindowHolder.Children.Add(sv);
            cc.Attach(balTransaction,balHospital,balInventory, applicationStatus, mode, value);
        }

    }
}
