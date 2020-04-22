  JumonyParser
  
  for (int i = 0; i <= 50; i++)
                {

               
                    string url = "https://en.resources.lenovo.com/hubsFront/ajax_loadAdditionalItems/?collectionId=1745478¤tPage="+i+"&limit=20&featuredItemCount=0&embedded=0";
                if (i == 0)
                {
                    url= "https://en.resources.lenovo.com/videos";
                }
                string json = "";
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
             
                    Encoding encoding = Encoding.UTF8;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    //request.Method = "get";
                    request.Headers.Add("Method", "GET");

                if (i != 0)
                {
                    //request.Connection="keep-alive";
                    request.Accept = "*/*";
                    request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    // request.UserAgent= "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36";
                    request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
                    request.Headers.Add("Cookie", "s_ecid=MCMID%7C09064801412016193040367766210413568793; oo_inv_support=1; gr_user_id=0df02bc4-b619-4d31-966e-6b69ebe3a550; grwng_uid=0c50958f-b854-4987-871f-cf8835825229; my_Recommend=%5B%7B%22Name%22%3A%22%u4E09%u5E74%u5230%u671F%u7535%u8111%u4E2A%u4EBA%u8D2D%u4E70%22%2C%22Url%22%3A%22http%3A//assets.lenovo.com/Biz/EmployeePurchase/Create.aspx%3Ftag%3DOASSO%22%2C%22Count%22%3A1%2C%22Date%22%3A1561116237434%7D%5D; eSupportProfile=nGLeVbdV9H5J812htyWzLZOPusjmwc7skt6YjwoABqpBIa9D7LYhPpU99%2FTTgMpm; s_fid=3043BC32DB7478CC-20097B78906C5A70; s_vi=[CS]v1|2E898EBE0507D001-6000010C8000A0EF[CE]; _vwo_uuid_v2=D60178971861F63169DE2FD8C641CFF2D|94c9b69ec6f6e9c5bb9e1c8120e47bb2; uf_privacy_prefs=1%7C1; pdf_event=WyJbe1widXVpZFwiOjU1MTU2NDAyNn0sMTU5NDQzMzM1NF0iLCI4OWMwY2IxOTY4NjkxNWIwZmJlNmY2ZjEyZDZjNWRiNCJd; _ufav=a64c856b188a40b28f7a8eb37f1833fe; _ga=GA1.2.50457710.1562897124; eloquautk=6259ce6a-6c46-4a5b-95fd-25581527aab2; aam_uuid=09191877347543808540382660653136827975; utag_main=v_id:016c0308b06600022d9d55c16f9903073005506b00978$_sn:1$_ss:1$_st:1563420796841$ses_id:1563418996841%3Bexp-session$_pn:1%3Bexp-session; _evga_5d7d=76bb0a9e7af2976f.; mbox=session#2fb77164db0648aba473c0b6ae3d00bc#1563420858|PC#2fb77164db0648aba473c0b6ae3d00bc.22_4#1626663798; aam_sc=aamsc%3D5406321%7C5408146%7C5408154%7C9620179%7C9562573%7C9620179%7C10464009; aamtest1=seg%3Dabc%2Cseg%3Ddef%2Cseg%3Dghi; aam_uuid=09191877347543808540382660653136827975; BVBRANDID=aaa41df3-9bbc-470c-8ba3-8c56e6240f1a; QuantumMetricUserID=5b8b664cced029717686801c53bbd99d; IR_PI=1563419019680.kxh9oms1pas%7C1563505419680; netotiate_vid=5d2fe1976f59e; _mkto_trk=id:183-WCT-620&token:_mch-lenovo.com-1563419034961-53723; __qca=P0-1867498818-1563419032209; Lenovo_SessionID=f26a810c-15f7-4ea6-8762-d74eb26bed59; AMCVS_F6171253512D2B8C0A490D45%40AdobeOrg=1; s_cc=true; eSupport.LWP=1; eSupport.CL=usen; oo_OODynamicRewrite_weight=0; _MGZ_=cp54tnodg0uhcd01smdko861gh; uiState={%22bannerDismissed%22:0}; _gid=GA1.2.574173531.1563504002; aam_sc=aamsc%3D5406321%7C5408146%7C5408154%7C9620179%7C9562573%7C9620179%7C10464009; s_sq=%5B%5BB%5D%5D; aamtest1=seg%3Dabc%2Cseg%3Ddef%2Cseg%3Dghi; _ufas=4680cfa09bf9461a86bf7beaaaa73104; s_dfa=lenovouspub%2Clenovoglobal; _gat=1; ufentry=20190719.015528; AMCV_F6171253512D2B8C0A490D45%40AdobeOrg=-1303530583%7CMCIDTS%7C18097%7CMCMID%7C09064801412016193040367766210413568793%7CMCAAMLH-1564118477%7C11%7CMCAAMB-1564118477%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1563520877s%7CNONE%7CMCAID%7CNONE%7CvVersion%7C3.3.0%7CMCCIDH%7C-1595058814; s_ppvl=ww_en%253Aen.resources%253Avideos%253Alenovo-xclarity-administrator-switch-management-final%2C41%2C100%2C17686%2C1536%2C754%2C1536%2C864%2C1.25%2CL; adcloud={%22_les_v%22:%22y%2Clenovo.com%2C1563515477%22}; s_ppv=ww_en%253Aen.resources%253Avideos%2C28%2C71%2C1907.5999755859375%2C1536%2C754%2C1536%2C864%2C1.25%2CL");
                }



                    
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        json = reader.ReadToEnd();
                    }
                if (json != "")
                {
                    IHtmlDocument source = new JumonyParser().Parse(json);

                    var li = source.Find("li");
                    List<Task> lTasks = new List<Task>();
                    foreach (var l in li)
                    {
                        if (l.Attribute("data-tags").Value() == "label=video")
                        {
                            LenovoPressVideo lenovoVideo = new LenovoPressVideo();
                            lenovoVideo.id = l.Attribute("data-id").Value();
                            lenovoVideo.duration = l.Find(".duration").FirstOrDefault().InnerHtml();
                            lenovoVideo.img = l.Find("div img").FirstOrDefault().Attribute("src").Value();
                            if (l.Find(".description")!= null)
                            {
                                lenovoVideo.title = l.Find(".description h3").FirstOrDefault().InnerText();
                                if (l.Find(".description h4").FirstOrDefault() != null)
                                {
                                    lenovoVideo.description = l.Find(".description h4").FirstOrDefault().InnerText();
                                }
                                else
                                {
                                    lenovoVideo.description = "";
                                }
                               
                            }
                            else
                            {
                                continue;
                                lenovoVideo.description = "";
                                lenovoVideo.title = "";
                            }
                           
                            lenovoVideo.view = l.Find(".item-link.view").FirstOrDefault().Attribute("href").Value();
                            lenovoVideo.date = DateTime.Parse(l.Find(".timeago").FirstOrDefault().Attribute("title").Value());


                            try
                            {
                                
                                string detail = "";
                                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                             
                                HttpWebRequest requestDetail = (HttpWebRequest)WebRequest.Create(lenovoVideo.view);
                                //request.Method = "get";
                                requestDetail.Headers.Add("Method", "GET");
                                HttpWebResponse responseDetail = (HttpWebResponse)requestDetail.GetResponse();
                                using (StreamReader readerDetail = new StreamReader(responseDetail.GetResponseStream(), Encoding.UTF8))
                                {
                                    detail = readerDetail.ReadToEnd();
                                }
                                if (detail != "")
                                {
                                    IHtmlDocument sourceDetail = new JumonyParser().Parse(detail);
                                    var sourceURl = sourceDetail.Find("iframe").FirstOrDefault();

                                    lenovoVideo.source = sourceURl.Attribute("src").Value();
                                }
                                  
                            }
                            catch (Exception)
                            {

                                
                            }
