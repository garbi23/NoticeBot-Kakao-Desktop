using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;

namespace NoticeBot
{
    public partial class Form1 : Form
    {

        KakaotalkActive[] katalkactive = new KakaotalkActive[100];





        public Form1()
        {
            InitializeComponent();

        }

        public void Button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string context = textBox2.Text;
            katalkactive[0] = new KakaotalkActive("우리자기", "테스트");
            katalkactive[3] = new KakaotalkActive("최태운", "테스트");

        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Notifications.Management.UserNotificationListener"))
            {
                // Listener supported!
            }

            else
            {
                // Older version of Windows, no Listener
            }
            // Get the listener
            UserNotificationListener listener = UserNotificationListener.Current;

            // And request access to the user's notifications (must be called from UI thread)
            UserNotificationListenerAccessStatus accessStatus = await listener.RequestAccessAsync();

            switch (accessStatus)
            {
                // This means the user has granted access.
                case UserNotificationListenerAccessStatus.Allowed:

                    // Yay! Proceed as normal
                    break;

                // This means the user has denied access.
                // Any further calls to RequestAccessAsync will instantly
                // return Denied. The user must go to the Windows settings
                // and manually allow access.
                case UserNotificationListenerAccessStatus.Denied:

                    // Show UI explaining that listener features will not
                    // work until user allows access.
                    break;

                // This means the user closed the prompt without
                // selecting either allow or deny. Further calls to
                // RequestAccessAsync will show the dialog again.
                case UserNotificationListenerAccessStatus.Unspecified:
                    // Show UI that allows the user to bring up the prompt again
                    break;
            }
            IReadOnlyList<UserNotification> notifs = await listener.GetNotificationsAsync(NotificationKinds.Toast);
        }
    }
}
