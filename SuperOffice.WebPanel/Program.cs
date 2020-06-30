using IdentityModel.OidcClient;
using SuperOffice.WebPanel.BackChannel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperOffice.WebPanel
{
    class Program
    {
        private static string AccessToken { get; set; }
        private static string RefreshToken { get; set; }
        private static string UserContext { get; set; }
        private static OidcClient _oidcClient;

        static void Main(string[] args) => WebPanelHandle().GetAwaiter().GetResult();

        public static async Task<Task> WebPanelHandle()
        {
            await Login();
            Console.WriteLine("\n");
            Console.WriteLine("Hello !");
            Console.WriteLine("Welcome ot web panel automation \n");
            Console.WriteLine("Enter 1 to get details of available web panels");
            Console.WriteLine("Enter 2 to create new web panel \n");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 1)
            {
                Console.WriteLine("Please wait while request is processing");
                var webPanelHandler = new WebPanelHandler();
                var webPanelList = await webPanelHandler.GetList(AccessToken, RefreshToken, UserContext);
                
                if (webPanelList.Any())
                {
                    Console.WriteLine($"You have {webPanelList.Count()} available webpanels \n");
                    foreach (var item in webPanelList)
                    {                        
                        Console.WriteLine($"Web Panel Name : {item.Name}, Web Panel ID : {item.WebPanelId}");
                    }
                }
                else
                {
                    Console.WriteLine($"No web panels available to display!");
                    Console.ReadKey();
                }
                
            }
            else if (input == 2)
            {

                Console.WriteLine("Enter name for the web panel");
                var webPanelName = Console.ReadLine().ToString();
                var webPanelHandler = new WebPanelHandler();
                var webPanel = await webPanelHandler.CreateWebPanel(AccessToken, RefreshToken, UserContext, webPanelName);

                if (webPanel != null)
                {
                    Console.WriteLine($"Web Panel Name Created succefully");
                    Console.WriteLine($"Web Panel Name : {webPanel.Name}, Web Panel ID : {webPanel.WebPanelId}");
                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
                Console.ReadKey();

            }

            return Task.CompletedTask;
        }

        private static async Task Login()
        {
            var browser = new Browser(44377);
            string redirectUri = "https://localhost:44377/desktop-callback/";

            var options = new OidcClientOptions
            {
                Authority = "https://sod.superoffice.com/login",
                LoadProfile = false,
                ClientId = "0c045f1855ea5197eaa0ba4d0d6a1db1",
                ClientSecret = "ad3a152032562d305d2f07f508401e1e",
                Scope = "openid",
                RedirectUri = redirectUri,
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.FormPost,
                Flow = OidcClientOptions.AuthenticationFlow.Hybrid,
                Browser = browser
            };

            options.Policy.Discovery.ValidateIssuerName = false;
            options.Policy.RequireAccessTokenHash = false;

            var client = new OidcClient(options);
            var state = await client.PrepareLoginAsync();

            Console.WriteLine($"Please wait while login...");
            _oidcClient = new OidcClient(options);
            var result = await _oidcClient.LoginAsync(new LoginRequest());

            if (!result.IsError)
            {
                GetTokens(result);
            }

            //LoginResult
        }

        private static void GetTokens(LoginResult loginResult)
        {
            if (loginResult != null && !loginResult.IsError)
            {
                AccessToken = loginResult.AccessToken;
                RefreshToken = loginResult.RefreshToken;
                UserContext = loginResult.User.Claims.FirstOrDefault(o => o.Type == "http://schemes.superoffice.net/identity/ctx").Value;
            }
        }
    }
}
