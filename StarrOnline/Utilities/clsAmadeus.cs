using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using k1aHostToolKit;
using k1aTST;
using s1aPNR;



namespace StarrOnline.Utilities
{
    class clsAmadeus
    {
        public string _errorStr = "";

        private HostSession objSession = new HostSession();
        private string strCurr = "";
        private bool isROE;
        PNR objPNR = new PNR();
        int tktCnt = 0;

        public bool connectASP()
        {
            HostSessions _Hosts = new HostSessions();

            try
            {
                if (_Hosts.Count > 0)
                {
                    objSession = _Hosts.UIActiveSession;
                    return true;
                }
                else
                {
                    _errorStr = "NO GDS connection";
                    return false;
                }
            }
            catch (Exception ex)
            {
                _errorStr = "NO GDS connection";
                return false;
            }
        }

        public bool isPresentROE()
        {
            return isROE;
        }

        public int ticketCount()
        {
            return tktCnt;
        }

        public Response GetLastFullReponse()
        {
            Response resp = new Response();
            String tempString = "";
            tempString = objSession.LastResponse.Buffer;
            resp.FullText = tempString;
            for (int ctr = 0, ctr2 = 1; ctr < tempString.Length; ctr += 80, ctr2++)
            {
                try
                {
                    ResponsePerLine line = new ResponsePerLine();
                    line.LineNumber = ctr2;
                    line.Text = tempString.Substring(ctr, 80);
                    resp.Texts.Add(line);
                }
                catch
                {
                    ResponsePerLine line = new ResponsePerLine();
                    line.LineNumber = ctr2; int y = 1;
                    if (tempString.Contains(")>"))
                    {
                        ctr = 0;
                        objSession.Send("md");
                        tempString = objSession.LastResponse.Buffer;
                        resp.FullText += tempString;
                        line.Text = tempString.Substring(ctr, 80);
                    }
                    else
                        line.Text = tempString.Substring(ctr, y);

                    resp.Texts.Add(line);
                }
            }
            resp.NumberOfLines = resp.Texts.Count;
            return resp;
        }

        public Response sendCmdWithResponseText(string var)
        {
            Response resp = new Response();
            String tempString = "";

            try
            {
                objSession.Send(var);
                //tempString = objSession.get_LastResponse().get_Buffer();
                resp.FullText = tempString;

                for (int ctr = 0, ctr2 = 1; ctr < tempString.Length; ctr += 80, ctr2++)
                {
                    try
                    {
                        ResponsePerLine line = new ResponsePerLine();
                        line.LineNumber = ctr2;
                        line.Text = tempString.Substring(ctr, 80);
                        resp.Texts.Add(line);
                    }
                    catch
                    {
                        ResponsePerLine line = new ResponsePerLine();
                        line.LineNumber = ctr2; int y = 1;
                        if (tempString.Contains(")>"))
                            y = 2;
                        line.Text = tempString.Substring(ctr, y);
                        resp.Texts.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }


            resp.NumberOfLines = resp.Texts.Count;
            return resp;

        }

        private int getPaxCount()
        {
            System.Collections.ArrayList paxes = new System.Collections.ArrayList();

            try
            {
                //objPNR.RetrievePNR(objSession, "rt" + strPNRData);

                foreach (s1aPNR.NameElement _NameElement in objPNR.NameElements)
                {
                    paxes.Add(_NameElement.LastName + " " + _NameElement.Initial);
                }

            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return paxes.Count;
        }

        public string getUserName()
        {
            string[] userName;

            sendCmdWithResponseText("JD");
            Response rsp = GetLastFullReponse();

            int i = 0;

            foreach (var x in rsp.Texts)
            {
                if (x.LineNumber == 5)
                {
                    userName = x.Text.Split(' ');

                    //return userName[68].ToString();

                    //foreach (var word in userName)
                    //{}

                    for (i = userName.GetLowerBound(0); i <= userName.GetUpperBound(0); i++)
                    {
                        if (i == 7)
                        {
                            return userName[i].Substring(0, 5);
                        }
                    }

                }

            }

            return "";
            //Split(objSession.Send(aspCmd).GetLineFromBuffer(lineNumber), " ")
        }

        public string getUser()
        {
            string _user = "";

            try
            {
                //objPNR.RetrieveCurrent(objSession);
                _user = objPNR.Header.Text;
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return _user;
        }

        public string getPNR()
        {
            string _pnr = "";

            try
            {
                objPNR.RetrieveCurrent(objSession);
                _pnr = objPNR.Header.RecordLocator;
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return _pnr;
        }

        public string getPax()
        {
            string allpax = "";
            System.Collections.ArrayList paxes = new System.Collections.ArrayList();
            //List<string> pnrsData = new List<string>(var.Split(','));

            try
            {

                //objPNR.RetrievePNR(objSession, "rt" + strData);

                foreach (s1aPNR.NameElement _NameElement in objPNR.NameElements)
                {
                    paxes.Add(_NameElement.LastName + "/" + _NameElement.Initial);
                }

                //int ctr = 0;

                //for (int i = 0; i < paxes.Count; i++)
                //{
                //    ctr += 1;
                //    allpax = allpax + "          " + ctr.ToString() + ". " + paxes[i] + "\n";
                //}
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return paxes[0].ToString();
        }

        private string decodeALFind(string var, bool varDo)
        {
            s1aGcEncodeDecode.GcCode ALgcCode = new s1aGcEncodeDecode.GcCode();
            s1aGcEncodeDecode.GcCodeInfos ALCodeInfos;
            s1aGcEncodeDecode.GcCodeInfo ALgcCodeInfo = new s1aGcEncodeDecode.GcCodeInfo();
            string airlineData = "";

            try
            {
                if (varDo)
                {
                    ALgcCode.RetrieveCodeInfos(var, s1aGcEncodeDecode.ecdGcCodeType.ecdAirline, s1aGcEncodeDecode.ecdEncodeDecodeWay.ecdDecode, objSession);
                    ALCodeInfos = ALgcCode.GcCodeInfos;

                    ALgcCodeInfo = ALCodeInfos.Item(0);

                    airlineData += ALgcCodeInfo.GcCodeDescription;
                }
                else
                {
                    airlineData = var;
                }
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return airlineData;


        }

        private string decodeCityFind(string var, bool varDo)
        {
            s1aGcEncodeDecode.GcCode cityGcCode = new s1aGcEncodeDecode.GcCode();
            s1aGcEncodeDecode.GcCodeInfos cityGcCodeInfos;
            s1aGcEncodeDecode.GcCodeInfo cityGcCodeInfo = new s1aGcEncodeDecode.GcCodeInfo();
            string cityData = "";

            try
            {
                if (varDo)
                {
                    cityGcCode.RetrieveCodeInfos(var, s1aGcEncodeDecode.ecdGcCodeType.ecdCity, s1aGcEncodeDecode.ecdEncodeDecodeWay.ecdDecode, objSession);
                    cityGcCodeInfos = cityGcCode.GcCodeInfos;

                    cityGcCodeInfo = cityGcCodeInfos.Item(0);

                    cityData += cityGcCodeInfo.GcCodeDescription;
                }
                else
                {
                    cityData = var;
                }
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return cityData;
        }

        private string ampmTimeConvert(DateTime var, bool varDo)
        {
            if (varDo)
                return String.Format("{0:hh:mm tt}", var);
            else
                return String.Format("{0:HH:mm}", var);
        }

        public int getItineraryCount(string strPNR)
        {
            PNR objPNR = new PNR();

            //List<string> pnrsData = new List<string>(strPNR.Split(','));

            //objPNR.RetrievePNR(objSession, "rt" + strPNR);

            objPNR.RetrieveCurrent(objSession);

            int itineraryNum = 0;

            foreach (s1aPNR.AirSegment _ItineraryElement in objPNR.AirSegments)
            {
                itineraryNum += 1;
            }

            return itineraryNum;
        }

        public string getBaggageAllowance(int segmentNum)
        {
            Response baggageData = new Response();
            string strDataReturn = "";
            PNR objPNR = new PNR();

            string strDataReturnTemp = "";

            TST objTST = new TST();

            objTST.RetrieveCurrentTst(objSession);

            int segmentCount = 1;

            try
            {
                foreach (k1aTST.AirSegment _baggageAllowance in objTST.AirSegments)
                {

                    strDataReturnTemp = _baggageAllowance.BaggageAllowance;

                    if (segmentCount == segmentNum)
                    {
                        strDataReturn = strDataReturnTemp;
                    }

                    segmentCount += 1;
                }
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return strDataReturn;
        }

        public string getItinerary()
        {
            string allitinerary = "";

            try
            {

                System.Collections.ArrayList itinerary = new System.Collections.ArrayList();

                PNR objPNR = new PNR();

                objPNR.RetrieveCurrent(objSession);

                int itineraryNum = 0;

                foreach (s1aPNR.AirSegment _ItineraryElement in objPNR.AirSegments)
                {
                    itineraryNum += 1;
                    if (itineraryNum == 1)
                    {
                        allitinerary = _ItineraryElement.BoardPoint.Trim() +  "/" + _ItineraryElement.OffPoint.Trim();
                    }else
                    {
                        allitinerary = allitinerary + "/" + _ItineraryElement.OffPoint.Trim();
                    }
                    
                    //itinerary.Add(_ItineraryElement.BoardPoint.Trim() + "/" + _ItineraryElement.OffPoint.Trim());
                }


                //return itinerary.ToString();
                return allitinerary;

            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return allitinerary;
        }

        public string getAirlineLocator(string var, bool decodeAL)
        {
            //string pnrReloc = "";
            string allItinerary = "";
            string newReference = "";

            PNR objPNR = new PNR();

            List<string> pnrsData = new List<string>(var.Split(','));

            System.Collections.ArrayList airSegments = new System.Collections.ArrayList();
            int ctr = 0;

            try
            {
                foreach (string strData in pnrsData)
                {
                    objPNR.RetrievePNR(objSession, "rt" + strData);

                    foreach (s1aPNR.AirSegment obj in objPNR.AirSegments)
                    {
                        string strAirLine = ((obj.AirlineReference == null) ? strData : obj.AirlineReference);
                        airSegments.Add(decodeALFind(obj.Airline, decodeAL) + "- " + strAirLine + "\n");
                    }

                    for (int i = 0; i < (airSegments.Count); i++)
                    {
                        ctr++;

                        if (newReference != airSegments[i].ToString())
                        {
                            allItinerary += airSegments[i] + "";
                            newReference = airSegments[i].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return allItinerary.ToUpper();
        }

        public string getTimeLimit(string var)
        {
            string ticketLimitData = "";
            string allTicketLimitData = "";

            PNR objPNR = new PNR();

            List<string> pnrsData = new List<string>(var.Split(','));

            try
            {
                foreach (string strData in pnrsData)
                {
                    objPNR.RetrievePNR(objSession, "rt" + strData);

                    foreach (s1aPNR.TicketElement tktl in objPNR.TicketElements)
                    {
                        if (tktl.Text.Contains("TK TL"))
                        {
                            ticketLimitData = tktl.Text.Remove(0, tktl.Text.IndexOf("TK TL") + 5).Trim().Substring(0, 5).Trim();
                        }
                        else if (tktl.Text.Contains("TK OK"))
                        {
                            ticketLimitData = tktl.Text.Remove(0, tktl.Text.IndexOf("TK OK") + 5).Trim().Substring(0, 5).Trim();
                        }

                        allTicketLimitData += strData.ToUpper() + " - " + ticketLimitData + "\n";
                    }
                }

            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return allTicketLimitData;
        }

        public string getCodeInfo(string var)
        {

            s1aGcEncodeDecode.GcCode sam = new s1aGcEncodeDecode.GcCode();
            s1aGcEncodeDecode.GcCodeInfos sam1;

            try
            {
                sam.RetrieveCodeInfos("PR", s1aGcEncodeDecode.ecdGcCodeType.ecdAirline, s1aGcEncodeDecode.ecdEncodeDecodeWay.ecdDecode, objSession);
            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }


            sam1 = sam.GcCodeInfos;

            string AlList = "";

            s1aGcEncodeDecode.GcCodeInfo gcInfo = new s1aGcEncodeDecode.GcCodeInfo();

            gcInfo = sam1.Item(0);

            AlList += gcInfo.GcCodeDescription;

            return AlList;
        }

        public string getBaggageAllowance(int lineNum, bool showBaggage)
        {
            Response baggageData = new Response();
            string strDataReturn = "";

            s1aPNR.PNR objPNR = new s1aPNR.PNR();

            try
            {
                if (showBaggage)
                {
                    objPNR.RetrieveCurrent(objSession);

                    baggageData = sendCmdWithResponseText("tqt");

                    //string l = baggageData[

                    string[] lines = baggageData.Texts[lineNum].Text.Split(new string[] { " " }, StringSplitOptions.None);

                    strDataReturn = lines[35].ToString() + lines[36].ToString() + lines[37].ToString() + lines[38].ToString() + lines[39].ToString() + lines[40].ToString() + lines[41].ToString() + lines[42].ToString() + lines[43].ToString() + lines[44].ToString() + lines[45].ToString() + lines[46].ToString() + lines[47].ToString() + lines[48].ToString() + lines[49].ToString() + lines[50].ToString();

                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return strDataReturn;
        }

        private string[] getRemarkElementData(String word, String strCode, String delim)
        {
            return Regex.Split(word, delim);
        }

        public String getlastRep()
        {
            return objSession.LastResponse.Buffer;
        }

        public string getFareCurr()
        {
            return strCurr;
        }

        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public string ConvertToDouble(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            if (isNum)
            {
                return retNum.ToString();
            }
            else
            {
                return " ";
            }
        }

        public double getTicketAmount()
        {
            k1aTST.TST _tst = new k1aTST.TST();
            String[] txtdata;
            string amt = "0.00";
            double roeAmt = 0;
            double ipp = 0;
            double totalAmt = 0;
            string currCode = "";

            _tst.RetrieveCurrentTst(objSession);


            if (_tst.TotalLineText != null)
            {

                txtdata = _tst.TotalLineText.ToString().Split(' ');
                currCode = _tst.TotalCurrencyCode;

                amt = Math.Round(_tst.TotalCollectionAmount, 2).ToString();

                if (currCode == "PHP")
                    roeAmt = Convert.ToDouble(_tst.TotalRateExchange.ToString());
                else
                    roeAmt = 1;

                isROE = true;


            }
            else
            {
                Response r = new Response();
                Match myMatchUSD;
                Match myMatchPHP;
                String[] strResp;
                bool startAmt = false;
                tktCnt = 0;

                double tktValue = 0;

                r = GetLastFullReponse();

                foreach (var x in r.Texts)
                {
                    myMatchPHP = Regex.Match(x.Text, "PHP", RegexOptions.IgnoreCase);
                    myMatchUSD = Regex.Match(x.Text, "USD", RegexOptions.IgnoreCase);

                    if (myMatchPHP.Success)
                    {
                        currCode = "PHP";
                    }

                    if (myMatchUSD.Success)
                    {
                        currCode = "USD";
                    }

                    if (myMatchPHP.Success || myMatchUSD.Success)
                    {
                        strResp = x.Text.Split(' ');

                        int lower = strResp.GetLowerBound(0);
                        int higher = strResp.GetUpperBound(0);
                        for (int i = lower; i < higher;)
                        {
                            //MessageBox.Show(i + " -- " + strResp[i]);

                            if (strResp[i] == "PHP" || strResp[i] == "USD")
                            {
                                startAmt = true;
                            }

                            if (startAmt)
                            {
                                if (strResp[i] != "")
                                {
                                    if (IsNumeric(strResp[i]))
                                    {
                                        tktCnt += 1;
                                        tktValue = Convert.ToDouble(strResp[i]) + tktValue;
                                        startAmt = false;
                                    }
                                }
                            }


                            i++;
                        }

                    }
                }

                amt = Math.Round(tktValue, 2).ToString();
                isROE = false;

            }

            if (currCode == "PHP" && isROE)
            {
                strCurr = currCode;
                ipp = 1.67 * roeAmt;
                totalAmt = Math.Round(Convert.ToDouble(amt) + ipp, 2);
                return Convert.ToDouble(totalAmt);

            }
            else if (currCode == "USD" && isROE)
            {
                strCurr = currCode;
                return (Convert.ToDouble(amt) + 1.67);

            }
            else
            {
                strCurr = currCode;
                return Convert.ToDouble(amt);

            }


            //using invd to gete amount
            //String[] txtdata;
            //double val = 0;
            //Response r = new Response();
            //sendCmdWithResponseText("invd");
            //r = GetLastFullReponse();
            //Match myMatch;
            //foreach (var x in r.Texts)
            //{
            //    myMatch = Regex.Match(x.Text, "AIR TOTAL", RegexOptions.IgnoreCase);
            //    if (myMatch.Success)
            //    {
            //        //MessageBox.Show(x.LineNumber.ToString() + "-" + x.Text);
            //        txtdata = x.Text.Split(' ');
            //        //strCurr = txtdata[29];
            //        strCurr = txtdata[46];
            //        //val = Convert.ToDouble(txtdata[38]);
            //        val = Convert.ToDouble(txtdata[56]);
            //    }
            //}
        }

        public string getDepartureTravelDate()
        {
            //objPNR.RetrieveCurrent(objSession);
            //System.Collections.ArrayList itinerary = new System.Collections.ArrayList();

            string arrDate = "";
            string returnDate = "";

            int itineraryCnt = 1;

            foreach (s1aPNR.AirSegment _itineraryElement in objPNR.AirSegments)
            {
                arrDate = String.Format("{0:dd-MM}", _itineraryElement.DepartureDate);
                if (itineraryCnt == 1)
                {
                    returnDate = arrDate;
                    itineraryCnt += 1;
                }
            }

            return returnDate;
        }

        public string getReturnTravelDate()
        {
            //objPNR.RetrieveCurrent(objSession);
            //System.Collections.ArrayList itinerary = new System.Collections.ArrayList();

            string arrDate = "";

            int itineraryCnt = 1;

            foreach (s1aPNR.AirSegment _itineraryElement in objPNR.AirSegments)
            {
                arrDate = String.Format("{0:dd-MM}", _itineraryElement.ArrivalDate);
            }

            return arrDate;
        }

        public string getRemarks(string varPNR, string varRMRCCode, string varRMRC)
        {
            //ConfidentialRemarkElement objRem;
            Match myMatch;
            String[] remData;
            String valToReturn = "";

            //PNR objPNR = new PNR();

            try
            {
                //objPNR.RetrievePNR(objSession, "rt" + varPNR);

                objSession.Send("RTR");

                foreach (RemarkElement objRem in objPNR.RemarkElements)
                {
                    myMatch = Regex.Match(objRem.FreeFlow, varRMRCCode, RegexOptions.IgnoreCase);
                    if (myMatch.Success)
                    {
                        remData = getRemarkElementData(objRem.FreeFlow, varRMRCCode, varRMRCCode);
                        valToReturn = remData[1];
                        return valToReturn;
                    }
                }

            }
            catch (Exception ex)
            {
                _errorStr = ex.Message;
            }

            return "";
        }

        public string getRM(string varPNR, string varRM)
        {
            Regex regex = new Regex("\\[(?<TextInsideBrackets>\\w+)\\]");
            string incomingValue = varRM;
            string insideBrackets = null;
            Match match = regex.Match(incomingValue);
            if (match.Success)
            {
                insideBrackets = match.Groups["TextInsideBrackets"].Value;
            }

            string strRMRC = insideBrackets.Substring(0, 2);
            string strRMRCCode = insideBrackets.Substring(2, 3);

            string strResult = getRemarks(varPNR, strRMRCCode, strRMRC);

            return strResult;
        }

    }
}
