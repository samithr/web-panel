using Newtonsoft.Json;
using SuperOffice.WebPanel.Enums;
using SuperOffice.WebPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SuperOffice.WebPanel
{
    public class WebPanelHandler
    {
        public async Task<IEnumerable<WebPanelEntity>> GetList(string accessToken, string refreshToken, string userContext)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    var url = Constants.SuperOfficeEnvironment + userContext + Constants.WebPanelUrl;
                    var response = await client.GetAsync(url);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<IEnumerable<WebPanelEntity>>(result);

                        // return not deleted web panels list
                        return data.Where(o => !o.Deleted.Value);
                    }

                    // handle access token expiration
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var newAccessToken = await GetAccessTokenByRefreshToken(refreshToken);
                        return await GetList(newAccessToken, refreshToken, userContext);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<WebPanelEntity> CreateWebPanel(string accessToken, string refreshToken, string userContext, string webPanelName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    using var client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                    var newWebPanelEntity = GetWebPanelObject(webPanelName);

                    var postData = JsonConvert.SerializeObject(newWebPanelEntity);
                    var buffer = Encoding.UTF8.GetBytes(postData);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var url = Constants.SuperOfficeEnvironment + userContext + Constants.WebPanelUrl;
                    var response = await client.PostAsync(url, byteContent);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<WebPanelEntity>(result);

                        // check if web panel is available
                        if (data != null && data.ProgId == Constants.ProgID)
                        {
                            await SetVisibleForUserGroups(data.WebPanelId.Value, accessToken, userContext, refreshToken);
                            return data;
                        }
                    }

                    // handle access token expiration
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var newAccessToken = await GetAccessTokenByRefreshToken(refreshToken);
                        return await CreateWebPanel(newAccessToken, refreshToken, userContext, webPanelName);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        /// <summary>
        /// create new web panel object
        /// </summary>
        /// <returns></returns>
        private WebPanelEntity GetWebPanelObject(string webPanelName)
        {
            try
            {
                var windowName = webPanelName.Replace(" ", "_");
                var newWebPanel = new WebPanelEntity()
                {
                    // set name fo webpanel, must be unique name
                    Name = webPanelName,
                    // set url to display
                    Url = Constants.UrlForWebPanelToDisplay,
                    //set location to display
                    VisibleIn = WebPanelEntityVisibleIn.CompanyMinicard,
                    // set window name, sould not contain spaces
                    WindowName = windowName,
                    // set url encoding
                    UrlEncoding = WebPanelEntityUrlEncoding.ANSI,
                    // set description for web panel
                    Tooltip = Constants.WebPanelDescription,
                    // show in status bar to open in separate window
                    ShowInStatusBar = true,
                    // set progID ot uniquely retrieve web panel
                    ProgId = Constants.ProgID,

                };
                return newWebPanel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get access token from auth service, using refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        private async Task<string> GetAccessTokenByRefreshToken(string refreshToken)
        {

            try
            {
                var accessToken = string.Empty;
                // get access token from refresh token
                if (!string.IsNullOrWhiteSpace(refreshToken))
                {
                    using var httpClient = new HttpClient();
                    var url = Constants.SuperOfficeEnvironment + Constants.RefreshTokenUri;
                    var clientID = Constants.ClientId;
                    var clientSecret = Constants.ClientSecret;
                    var redirectUrl = Constants.RefreshTokenRedirectUrl;
                    url += $"client_id={ clientID}&client_secret={clientSecret}&refresh_token={refreshToken}&redirect_url={redirectUrl}";

                    var response = await httpClient.PostAsync(url, null);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<RefreshTokenResponse>(result);
                        if (!string.IsNullOrEmpty(data.access_token))
                        {
                            accessToken = data.access_token;
                        }
                    }
                }
                return accessToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// set web panel visibel for selected user groups
        /// </summary>
        private async Task<bool> SetVisibleForUserGroups(int listItemID, string accessToken, string userContext, string refreshToken)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var url = Constants.SuperOfficeEnvironment + userContext + Constants.UserGroupUrl + listItemID + Constants.UserGroups;

                // get user groups list
                var userGroups = await client.GetAsync(url);

                if (userGroups.StatusCode == HttpStatusCode.OK)
                {
                    var result = userGroups.Content.ReadAsStringAsync().Result;
                    var userGroupData = JsonConvert.DeserializeObject<IEnumerable<UserGroups>>(result);

                    var userGroupCollection = new List<UserGroups>();

                    foreach (var item in userGroupData)
                    {
                        //  set visibility true for all user groups
                        var userGroupItem = new UserGroups()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Selected = true
                        };
                        userGroupCollection.Add(userGroupItem);
                    }

                    var postData = JsonConvert.SerializeObject(userGroupCollection);
                    var content = new StringContent(postData, Encoding.UTF8, "application/json");

                    // update usergourp list , web panel visible for
                    var response = await client.PutAsync(url, content);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
                // if access fails while updating user groups try agian with new acccess token
                if (userGroups.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var newAccessToken = await GetAccessTokenByRefreshToken(refreshToken);
                    return await SetVisibleForUserGroups(listItemID, newAccessToken, userContext, refreshToken);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
