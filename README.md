# web-panel
This solution is a simple console application created using .net core 3.1 to demonstrate web panel creating process
# Steps to run the solution
1. Edit the Constants.cs file to add ClientId, ClientSecret
2. Start the console application
3. Complete authentication process
4. Enter 1 for get the details of available web panels
5. Else enter 2 for create new web panel
  a). Enter name for web panel to be created (name must be unique)
  b). when process is completed, you will see the details of created web panel on console (Name, Id)
  c). Check your site for newly created web panel shown in mini card
  
 ** Tips
 - You can change the preview of web panel by changing "UrlForWebPanelToDisplay" in Constants.cs file
 - Also you can change the location web panel is shown by changing "VisibleIn", location should be seected from "WebPanelEntityVisibleIn" enum
 - "Tooltip" is shown as description in web panel
 - If you set "ShowInStatusBar" to true, url on your web panel can be open in new tab
