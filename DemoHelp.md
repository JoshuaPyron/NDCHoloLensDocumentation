#Hello, I see you need my help.

#### Setting up the needed Demo Information
1.	Open the app at least once. Hopefully, we will put the latest build on it before you leave.
2.	Place the two menus in the location you want them for the presentation.
3.	Close the app by clicking on the exit button. If you simply do the “bloom” gesture, the values will not be saved, because the app will still be running in the background.
4.	Connect the HoloLens to a computer via the port on the back left of the HoloLens
5.	In your web browser go to the following link: http://127.0.0.1:10080/FileExplorer.htm
6.	Go to the following directory to find the DemoInfo text file: LocalAppData\PrimaryHoloProject_1.0.0.0_x86__pt6n26qfys7q8\LocalState
7.	Download the existing DemoInfo file and alter the values that you want
  a.	Simulate_Product_Data should be set to “True” if you do not have access to an instrument
  b.	Sharing_IP is the IPv4 address of the machine running SharingService.exe. If this is your laptop, input that IP. If SharingService.exe is running on a server, put the server address here.
  c.	Product_IP is the IPv4 of the device that you want to be displayed on the Gauge Menu. If Simulate_Product_Data is True, you can simply leave this line alone.
8.	Delete the DemoInfo file in the HoloLens (using the web browser)
9.	In “Upload a file to this directory”, click “Choose File”, select the modified DemoInfo file, and click Upload.
