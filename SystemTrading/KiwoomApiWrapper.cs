using System.ComponentModel;
using System;

using AxKHOpenAPILib;
using System.Globalization;
using System.Linq;

namespace SystemTrading
{

    public enum KiwoomError
    {
        [Description("정상처리")] OP_ERR_NONE = 0,
        [Description("실패")] OP_ERR_FAIL = -10,
        [Description("사용자정보교환실패")] OP_ERR_LOGIN = -100,
        [Description("서버접속실패")] OP_ERR_CONNECT = -101,
        [Description("버전처리실패")] OP_ERR_VERSION = -102,
        [Description("개인방화벽실패")] OP_ERR_FIREWALL = -103,
        [Description("메모리보호실패")] OP_ERR_MEMORY = -104,
        [Description("함수입력값오류")] OP_ERR_INPUT = -105,
        [Description("통신연결종료")] OP_ERR_SOCKET_CLOSED = -106,
        [Description("시세조회과부하")] OP_ERR_SISE_OVERFLOW = -200,
        [Description("전문작성초기화실패")] OP_ERR_RQ_STRUCT_FAIL = -201,
        [Description("전문작성입력값오류")] OP_ERR_RQ_STRING_FAIL = -202,
        [Description("데이터없음.")] OP_ERR_NO_DATA = -203,
        [Description("조회가능한종목수초과")] OP_ERR_OVER_MAX_DATA = -204,
        [Description("데이터수신실패")] OP_ERR_DATA_RCV_FAIL = -205,
        [Description("조회가능한FID수초과")] OP_ERR_OVER_MAX_FID = -206,
        [Description("실시간해제오류")] OP_ERR_REAL_CANCEL = -207,
        [Description("입력값오류")] OP_ERR_ORD_WRONG_INPUT = -300,
        [Description("계좌비밀번호없음")] OP_ERR_ORD_WRONG_ACCTNO = -301,
        [Description("타인계좌사용오류")] OP_ERR_OTHER_ACC_USE = -302,
        [Description("주문가격이20억원을초과")] OP_ERR_MIS_2BILL_EXC = -303,
        [Description("주문가격이50억원을초과")] OP_ERR_MIS_5BILL_EXC = -304,
        [Description("주문수량이총발행주수의1%초과오류")] OP_ERR_MIS_1PER_EXC = -305,
        [Description("주문수량은총발행주수의3%초과오류")] OP_ERR_MIS_3PER_EXC = -306,
        [Description("주문전송실패")] OP_ERR_SEND_FAIL = -307,
        [Description("주문전송과부하")] OP_ERR_ORD_OVERFLOW = -308,
        [Description("주문수량300계약초과")] OP_ERR_MIS_300CNT_EXC = -309,
        [Description("주문수량500계약초과")] OP_ERR_MIS_500CNT_EXC = -310,
        [Description("계좌정보없음")] OP_ERR_ORD_WRONG_ACCTINFO = -340,
        [Description("종목코드없음")] OP_ERR_ORD_SYMCODE_EMPTY = -500
    }


    public class KiwoomAPI
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            );

        private AxKHOpenAPI _openApi;
        public delegate void OnConnect(bool result);
        public OnConnect OnConnectEventHandler;

        private int _rtmessageCounter;

        public delegate void OnRealTimeMessageCountModified(int newCount);
        public OnRealTimeMessageCountModified OnRealTimeMessageCountModifiedHandler;

        public int RealTimeMessageCounter
        {
            get
            {
                return _rtmessageCounter;
            }
        }

        public KiwoomAPI(AxKHOpenAPI openApi)
        {
            _openApi = openApi;
            _openApi.OnEventConnect += OnEventConnectEventHandler;
            _openApi.OnReceiveTrData += OnReceiveTrDataEventHandler;
            _openApi.OnReceiveRealData += OnReceiveRealDataEventHandler;

            OnConnectEventHandler = initialize;

            SerDeUtil.PublishRealTimeStructure(new TrStockPrice());
        }

        public int CommConnect()
        {
            return _openApi.CommConnect();
        }

        private string GetCommRealData(string realType, int fid)
        {
            return _openApi.GetCommRealData(realType, fid);
        }

        private string GetCommRealDataString(string realType, int fid)
        {
            return _openApi.GetCommRealData(realType, fid);
        }

        private int GetCommRealDataInt(string realType, int fid)
        {
            try
            {
                return Int32.Parse(_openApi.GetCommRealData(realType, fid));
            }
            catch (Exception)
            {
                log.WarnFormat("TYPE CONVERSION FAILED. REAL_TYPE: #{0}#, FID: #{1}#, REAL_VALUE: #{2}#", realType, fid, _openApi.GetCommRealData(realType, fid));
                return 0;
            }
        }

        private long GetCommRealDataLong(string realType, int fid)
        {
            try
            {
                return long.Parse(_openApi.GetCommRealData(realType, fid));
            }
            catch(Exception)
            {
                log.WarnFormat("TYPE CONVERSION FAILED. REAL_TYPE: #{0}#, FID: #{1}#, REAL_VALUE: #{2}#", realType, fid, _openApi.GetCommRealData(realType, fid));
                return 0;
            }
        }

        private float GetCommRealDataFloat(string realType, int fid)
        {
            try
            {
                return float.Parse(_openApi.GetCommRealData(realType, fid));
            }
            catch (Exception)
            {
                log.WarnFormat("TYPE CONVERSION FAILED. REAL_TYPE: #{0}#, FID: #{1}#, REAL_VALUE: #{2}#", realType, fid, _openApi.GetCommRealData(realType, fid));
                return 0f;
            }
        }

        private void OnEventConnectEventHandler(object sender, _DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            switch ((KiwoomError)e.nErrCode)
            {
                case KiwoomError.OP_ERR_NONE:
                    // TODO : Success
                    OnConnectEventHandler(true);
                    break;
                default:
                    OnConnectEventHandler(false);
                    Console.WriteLine("Error Code :" + KiwoomUtil.GetDescription<KiwoomError>((KiwoomError)e.nErrCode));
                    break;
            }
        }

        public bool IsConnected()
        {
            return _openApi.GetConnectState() == 1;
        }

        private static string[] REALTIME_SCREEN_NUMBER_LIST = { "000000", "000001", "000002", "000003", "000004", "000005" };

        private static string[] REALTIME_FID_LIST = {
            "10",   // 현재가__실시간종가
            "27",   // _최우선_매도호가
            "181",  // 미결제_약정_전일대비
            "214",  // 장시작 예상잔여시간
            "216",  // 투자자별 ticker
            "131"   // 시간외 매도호가 총잔량
        };
        

        public void RegisterRealTime(string[] registered)
        {
            string registeredTarget = "";
            string registeredFid = String.Join(";", REALTIME_FID_LIST);
            for (int cnt = 0; cnt < ((registered.Length / 100) + 1); cnt++)
            {
                registeredTarget = String.Join(";",
                    registered
                    .Skip<string>(cnt * 100)
                    .Take<string>(((cnt+1) * 100 < registered.Length ? 100 : registered.Length - cnt * 100)));
                _openApi.SetRealReg(
                    "0000", // REALTIME_SCREEN_NUMBER_LIST[cnt],
                    registeredTarget,
                    registeredFid,
                    (cnt == 0 ? "0" : "1"));
            }
        }

        public void UnRegisterRealTime()
        {
            _openApi.SetRealReg("0000", "005930", String.Join(";", REALTIME_FID_LIST), "0");
            _openApi.SetRealRemove("0000", "005930");
        }

        public void Disconnect()
        {
            // _openApi.CommTerminate();    // Not Supported. 
        }

        private void OnReceiveTrDataEventHandler(object sender, _DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            
        }

        private void OnReceiveRealDataEventHandler(object sender, _DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            TrStructure rtn = null;
            switch(e.sRealType)
            {
                case "주식시세":
                    { TrStockPrice record = new TrStockPrice();

                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.prevday_contrast = GetCommRealDataFloat(e.sRealType, 11);
                        record.hlratio = GetCommRealDataFloat(e.sRealType, 12);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.acctvalue = GetCommRealDataLong(e.sRealType, 14);
                        record.currprice = GetCommRealDataFloat(e.sRealType, 16);
                        record.high = GetCommRealDataFloat(e.sRealType, 17);
                        record.low = GetCommRealDataFloat(e.sRealType, 18);
                        record.contpervday_symbol = GetCommRealDataString(e.sRealType, 25);
                        record.prevtxamt_contrast_contract = GetCommRealDataFloat(e.sRealType, 26);
                        record.tvalue_inde = GetCommRealDataFloat(e.sRealType, 29);
                        record.prevtxamt_contrast_ratio = GetCommRealDataFloat(e.sRealType, 30);
                        record.toratio = GetCommRealDataFloat(e.sRealType, 31);
                        record.tfee = GetCommRealDataLong(e.sRealType, 32);
                        record.currmc = GetCommRealDataLong(e.sRealType, 311);
                        rtn = record; break;
                    }
                case "주식체결":
                    { TrStockTransaction record = new TrStockTransaction();

                        record.ttime_hhmmss = GetCommRealDataString(e.sRealType, 20);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.prevday_contrast = GetCommRealDataFloat(e.sRealType, 11);
                        record.hlratio = GetCommRealDataFloat(e.sRealType, 12);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.tamount = GetCommRealDataLong(e.sRealType, 15);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.acctvalue = GetCommRealDataLong(e.sRealType, 14);
                        record.currprice = GetCommRealDataFloat(e.sRealType, 16);
                        record.high = GetCommRealDataFloat(e.sRealType, 17);
                        record.low = GetCommRealDataFloat(e.sRealType, 18);
                        record.contpervday_symbol = GetCommRealDataString(e.sRealType, 25);
                        record.prevtxamt_contrast_contract = GetCommRealDataFloat(e.sRealType, 26);
                        record.tvalue_inde = GetCommRealDataFloat(e.sRealType, 29);
                        record.prevtxamt_contrast_ratio = GetCommRealDataFloat(e.sRealType, 30);
                        record.toratio = GetCommRealDataFloat(e.sRealType, 31);
                        record.tfee = GetCommRealDataLong(e.sRealType, 32);
                        record.tintensity = GetCommRealDataLong(e.sRealType, 228);
                        record.currmc = GetCommRealDataLong(e.sRealType, 311);
                        record.mtype = GetCommRealDataString(e.sRealType, 290);
                        record.k_o_approx = GetCommRealDataFloat(e.sRealType, 691);
                        rtn = record; break;
                    }
                case "주식우선호가":
                    { TrStockFirstCallPrice record = new TrStockFirstCallPrice();

                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        rtn = record; break;
                    }
                case "주식호가잔량":
                    { TrStockSpread record = new TrStockSpread();

                        record.quotetime = GetCommRealDataString(e.sRealType, 21);
                        record.bid1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.bid_amt1 = GetCommRealDataFloat(e.sRealType, 61);
                        record.bid_contrastprevtick1 = GetCommRealDataFloat(e.sRealType, 81);
                        record.ask1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.ask_amt1 = GetCommRealDataFloat(e.sRealType, 71);
                        record.ask_contrastprevtick1 = GetCommRealDataFloat(e.sRealType, 91);
                        record.bid2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.bid_amt2 = GetCommRealDataFloat(e.sRealType, 62);
                        record.bid_contrastprevtick2 = GetCommRealDataFloat(e.sRealType, 82);
                        record.ask2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.ask_amt2 = GetCommRealDataFloat(e.sRealType, 72);
                        record.ask_contrastprevtick2 = GetCommRealDataFloat(e.sRealType, 92);
                        record.bid3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.bid_amt3 = GetCommRealDataFloat(e.sRealType, 63);
                        record.bid_contrastprevtick3 = GetCommRealDataFloat(e.sRealType, 83);
                        record.ask3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.ask_amt3 = GetCommRealDataFloat(e.sRealType, 73);
                        record.ask_contrastprevtick3 = GetCommRealDataFloat(e.sRealType, 93);
                        record.bid4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.bid_amt4 = GetCommRealDataFloat(e.sRealType, 64);
                        record.bid_contrastprevtick4 = GetCommRealDataFloat(e.sRealType, 84);
                        record.ask4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.ask_amt4 = GetCommRealDataFloat(e.sRealType, 74);
                        record.ask_contrastprevtick4 = GetCommRealDataFloat(e.sRealType, 94);
                        record.bid5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.bid_amt5 = GetCommRealDataFloat(e.sRealType, 65);
                        record.bid_contrastprevtick5 = GetCommRealDataFloat(e.sRealType, 85);
                        record.ask5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.ask_amt5 = GetCommRealDataFloat(e.sRealType, 75);
                        record.ask_contrastprevtick5 = GetCommRealDataFloat(e.sRealType, 95);
                        record.bid6 = GetCommRealDataFloat(e.sRealType, 46);
                        record.bid_amt6 = GetCommRealDataFloat(e.sRealType, 66);
                        record.bid_contrastprevtick6 = GetCommRealDataFloat(e.sRealType, 86);
                        record.ask6 = GetCommRealDataFloat(e.sRealType, 56);
                        record.ask_amt6 = GetCommRealDataFloat(e.sRealType, 76);
                        record.ask_contrastprevtick6 = GetCommRealDataFloat(e.sRealType, 96);
                        record.bid7 = GetCommRealDataFloat(e.sRealType, 47);
                        record.bid_amt7 = GetCommRealDataFloat(e.sRealType, 67);
                        record.bid_contrastprevtick7 = GetCommRealDataFloat(e.sRealType, 87);
                        record.ask7 = GetCommRealDataFloat(e.sRealType, 57);
                        record.ask_amt7 = GetCommRealDataFloat(e.sRealType, 77);
                        record.ask_contrastprevtick7 = GetCommRealDataFloat(e.sRealType, 97);
                        record.bid8 = GetCommRealDataFloat(e.sRealType, 48);
                        record.bid_amt8 = GetCommRealDataFloat(e.sRealType, 68);
                        record.bid_contrastprevtick8 = GetCommRealDataFloat(e.sRealType, 88);
                        record.ask8 = GetCommRealDataFloat(e.sRealType, 58);
                        record.ask_amt8 = GetCommRealDataFloat(e.sRealType, 78);
                        record.ask_contrastprevtick8 = GetCommRealDataFloat(e.sRealType, 98);
                        record.bid9 = GetCommRealDataFloat(e.sRealType, 49);
                        record.bid_amt9 = GetCommRealDataFloat(e.sRealType, 69);
                        record.bid_contrastprevtick9 = GetCommRealDataFloat(e.sRealType, 89);
                        record.ask9 = GetCommRealDataFloat(e.sRealType, 59);
                        record.ask_amt9 = GetCommRealDataFloat(e.sRealType, 79);
                        record.ask_contrastprevtick9 = GetCommRealDataFloat(e.sRealType, 99);
                        record.bid10 = GetCommRealDataFloat(e.sRealType, 50);
                        record.bid_amt10 = GetCommRealDataFloat(e.sRealType, 70);
                        record.bid_contrastprevtick10 = GetCommRealDataFloat(e.sRealType, 90);
                        record.ask10 = GetCommRealDataFloat(e.sRealType, 60);
                        record.ask_amt10 = GetCommRealDataFloat(e.sRealType, 80);
                        record.ask_contrastprevtick10 = GetCommRealDataFloat(e.sRealType, 100);
                        record.bid_tredis = GetCommRealDataLong(e.sRealType, 121);
                        record.bid_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 122);
                        record.ask_tredis = GetCommRealDataLong(e.sRealType, 125);
                        record.ask_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 126);
                        record.estimcontractprice_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.estimcontract_amt = GetCommRealDataFloat(e.sRealType, 24);
                        record.netaskspread_taskresids_tbidresids = GetCommRealDataLong(e.sRealType, 128);
                        record.askratio = GetCommRealDataFloat(e.sRealType, 129);
                        record.netbidspread_tbidresids_taskresids = GetCommRealDataLong(e.sRealType, 138);
                        record.bidratio = GetCommRealDataFloat(e.sRealType, 139);
                        record.estimcontractprice_prevdayclose_contrast = GetCommRealDataFloat(e.sRealType, 200);
                        record.estimcontractprice_prevdayclose_contrast_hlratio = GetCommRealDataFloat(e.sRealType, 201);
                        record.estimcontractprice_prevdayclose_contrastsymbol = GetCommRealDataFloat(e.sRealType, 238);
                        record.estimcontractprice = GetCommRealDataFloat(e.sRealType, 291);
                        record.estimcontractamt = GetCommRealDataFloat(e.sRealType, 292);
                        record.estimcontractprice_contpervday_symbol = GetCommRealDataString(e.sRealType, 293);
                        record.estimcontractprice_contpervday = GetCommRealDataFloat(e.sRealType, 294);
                        record.estimcontractprice_contpervday_hlratio = GetCommRealDataFloat(e.sRealType, 295);
                        record.lpbid_amt1 = GetCommRealDataFloat(e.sRealType, 621);
                        record.lpask_amt1 = GetCommRealDataFloat(e.sRealType, 631);
                        record.lpbid_amt2 = GetCommRealDataFloat(e.sRealType, 622);
                        record.lpask_amt2 = GetCommRealDataFloat(e.sRealType, 632);
                        record.lpbid_amt3 = GetCommRealDataFloat(e.sRealType, 623);
                        record.lpask_amt3 = GetCommRealDataFloat(e.sRealType, 633);
                        record.lpbid_amt4 = GetCommRealDataFloat(e.sRealType, 624);
                        record.lpask_amt4 = GetCommRealDataFloat(e.sRealType, 634);
                        record.lpbid_amt5 = GetCommRealDataFloat(e.sRealType, 625);
                        record.lpask_amt5 = GetCommRealDataFloat(e.sRealType, 635);
                        record.lpbid_amt6 = GetCommRealDataFloat(e.sRealType, 626);
                        record.lpask_amt6 = GetCommRealDataFloat(e.sRealType, 636);
                        record.lpbid_amt7 = GetCommRealDataFloat(e.sRealType, 627);
                        record.lpask_amt7 = GetCommRealDataFloat(e.sRealType, 637);
                        record.lpbid_amt8 = GetCommRealDataFloat(e.sRealType, 628);
                        record.lpask_amt8 = GetCommRealDataFloat(e.sRealType, 638);
                        record.lpbid_amt9 = GetCommRealDataFloat(e.sRealType, 629);
                        record.lpask_amt9 = GetCommRealDataFloat(e.sRealType, 639);
                        record.lpbid_amt10 = GetCommRealDataFloat(e.sRealType, 630);
                        record.lpask_amt10 = GetCommRealDataFloat(e.sRealType, 640);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.estim_contractratio_prevtxamt = GetCommRealDataFloat(e.sRealType, 299);
                        record.mmngttype = GetCommRealDataString(e.sRealType, 215);
                        record.ptrader_ticker = GetCommRealDataString(e.sRealType, 216);
                        rtn = record; break;
                    }
                case "선물시세":
                    { TrFuturePrice record = new TrFuturePrice();

                        record.ttime_hhmmss = GetCommRealDataString(e.sRealType, 20);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.prevday_contrast = GetCommRealDataFloat(e.sRealType, 11);
                        record.hlratio = GetCommRealDataFloat(e.sRealType, 12);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.tamount = GetCommRealDataLong(e.sRealType, 15);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.acctvalue = GetCommRealDataLong(e.sRealType, 14);
                        record.currprice = GetCommRealDataFloat(e.sRealType, 16);
                        record.high = GetCommRealDataFloat(e.sRealType, 17);
                        record.low = GetCommRealDataFloat(e.sRealType, 18);
                        record.opened_agr = GetCommRealDataLong(e.sRealType, 195);
                        record.theoprice = GetCommRealDataFloat(e.sRealType, 182);
                        record.theobasis = GetCommRealDataFloat(e.sRealType, 184);
                        record.ask_trader_color3 = GetCommRealDataString(e.sRealType, 183);
                        record.disjunctratio = GetCommRealDataFloat(e.sRealType, 186);
                        record.opened_agr_contpervday = GetCommRealDataLong(e.sRealType, 181);
                        record.disjunction = GetCommRealDataFloat(e.sRealType, 185);
                        record.contpervday_symbol = GetCommRealDataString(e.sRealType, 25);
                        record.kospi200 = GetCommRealDataFloat(e.sRealType, 197);
                        record.prevtxamt_contrast_contract = GetCommRealDataFloat(e.sRealType, 26);
                        record.open_opened_agramt = GetCommRealDataFloat(e.sRealType, 246);
                        record.max_opened_agramt = GetCommRealDataFloat(e.sRealType, 247);
                        record.min_opened_agramt = GetCommRealDataFloat(e.sRealType, 248);
                        record.prevtxamt_contrast_ratio = GetCommRealDataFloat(e.sRealType, 30);
                        record.opened_inde = GetCommRealDataLong(e.sRealType, 196);
                        rtn = record; break;
                    }
                case "선물호가잔량":
                    { TrFutureQuoteSpread record = new TrFutureQuoteSpread();

                        record.quotetime = GetCommRealDataString(e.sRealType, 21);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.bid1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.bid_amt1 = GetCommRealDataFloat(e.sRealType, 61);
                        record.bid_contrastprevtick1 = GetCommRealDataFloat(e.sRealType, 81);
                        record.bid_count1 = GetCommRealDataFloat(e.sRealType, 101);
                        record.ask1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.ask_amt1 = GetCommRealDataFloat(e.sRealType, 71);
                        record.ask_contrastprevtick1 = GetCommRealDataFloat(e.sRealType, 91);
                        record.ask_count1 = GetCommRealDataFloat(e.sRealType, 111);
                        record.bid2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.bid_amt2 = GetCommRealDataFloat(e.sRealType, 62);
                        record.bid_contrastprevtick2 = GetCommRealDataFloat(e.sRealType, 82);
                        record.bid_count2 = GetCommRealDataFloat(e.sRealType, 102);
                        record.ask2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.ask_amt2 = GetCommRealDataFloat(e.sRealType, 72);
                        record.ask_contrastprevtick2 = GetCommRealDataFloat(e.sRealType, 92);
                        record.ask_count2 = GetCommRealDataFloat(e.sRealType, 112);
                        record.bid3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.bid_amt3 = GetCommRealDataFloat(e.sRealType, 63);
                        record.bid_contrastprevtick3 = GetCommRealDataFloat(e.sRealType, 83);
                        record.bid_count3 = GetCommRealDataFloat(e.sRealType, 103);
                        record.ask3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.ask_amt3 = GetCommRealDataFloat(e.sRealType, 73);
                        record.ask_contrastprevtick3 = GetCommRealDataFloat(e.sRealType, 93);
                        record.ask_count3 = GetCommRealDataFloat(e.sRealType, 113);
                        record.bid4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.bid_amt4 = GetCommRealDataFloat(e.sRealType, 64);
                        record.bid_contrastprevtick4 = GetCommRealDataFloat(e.sRealType, 84);
                        record.bid_count4 = GetCommRealDataFloat(e.sRealType, 104);
                        record.ask4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.ask_amt4 = GetCommRealDataFloat(e.sRealType, 74);
                        record.ask_contrastprevtick4 = GetCommRealDataFloat(e.sRealType, 94);
                        record.ask_count4 = GetCommRealDataFloat(e.sRealType, 114);
                        record.bid5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.bid_amt5 = GetCommRealDataFloat(e.sRealType, 65);
                        record.bid_contrastprevtick5 = GetCommRealDataFloat(e.sRealType, 85);
                        record.bid_count5 = GetCommRealDataFloat(e.sRealType, 105);
                        record.ask5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.ask_amt5 = GetCommRealDataFloat(e.sRealType, 75);
                        record.ask_contrastprevtick5 = GetCommRealDataFloat(e.sRealType, 95);
                        record.ask_count5 = GetCommRealDataFloat(e.sRealType, 115);
                        record.bid_tredis = GetCommRealDataLong(e.sRealType, 121);
                        record.bid_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 122);
                        record.bid_tcount = GetCommRealDataLong(e.sRealType, 123);
                        record.ask_tredis = GetCommRealDataLong(e.sRealType, 125);
                        record.ask_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 126);
                        record.ask_tcount = GetCommRealDataLong(e.sRealType, 127);
                        record.quoteprice_netspread = GetCommRealDataLong(e.sRealType, 137);
                        record.netaskspread_taskresids_tbidresids = GetCommRealDataLong(e.sRealType, 128);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.estimcontractprice_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.estimcontractprice_prevdayclose_contrastsymbol = GetCommRealDataFloat(e.sRealType, 238);
                        record.estimcontractprice_prevdayclose_contrast = GetCommRealDataFloat(e.sRealType, 200);
                        record.estimcontractprice_prevdayclose_contrast_hlratio = GetCommRealDataFloat(e.sRealType, 201);
                        record.estimcontractprice = GetCommRealDataFloat(e.sRealType, 291);
                        record.estimcontractprice_contpervday_symbol = GetCommRealDataString(e.sRealType, 293);
                        record.estimcontractprice_contpervday = GetCommRealDataFloat(e.sRealType, 294);
                        record.estimcontractprice_contpervday_hlratio = GetCommRealDataFloat(e.sRealType, 295);
                        rtn = record; break;
                    }
                case "선물이론가":
                    { TrFutureTheoPrice record = new TrFutureTheoPrice();

                        record.opened_agr = GetCommRealDataLong(e.sRealType, 195);
                        record.theoprice = GetCommRealDataFloat(e.sRealType, 182);
                        record.theobasis = GetCommRealDataFloat(e.sRealType, 184);
                        record.ask_trader_color3 = GetCommRealDataString(e.sRealType, 183);
                        record.disjunctratio = GetCommRealDataFloat(e.sRealType, 186);
                        record.opened_agr_contpervday = GetCommRealDataLong(e.sRealType, 181);
                        record.disjunction = GetCommRealDataFloat(e.sRealType, 185);
                        record.open_opened_agramt = GetCommRealDataFloat(e.sRealType, 246);
                        record.max_opened_agramt = GetCommRealDataFloat(e.sRealType, 247);
                        record.min_opened_agramt = GetCommRealDataFloat(e.sRealType, 248);
                        rtn = record; break;
                    }
                case "옵션시세":
                    { TrOptionPrice record = new TrOptionPrice();

                        record.ttime_hhmmss = GetCommRealDataString(e.sRealType, 20);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.prevday_contrast = GetCommRealDataFloat(e.sRealType, 11);
                        record.hlratio = GetCommRealDataFloat(e.sRealType, 12);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.tamount = GetCommRealDataLong(e.sRealType, 15);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.acctvalue = GetCommRealDataLong(e.sRealType, 14);
                        record.currprice = GetCommRealDataFloat(e.sRealType, 16);
                        record.high = GetCommRealDataFloat(e.sRealType, 17);
                        record.low = GetCommRealDataFloat(e.sRealType, 18);
                        record.opened_agr = GetCommRealDataLong(e.sRealType, 195);
                        record.theoprice = GetCommRealDataFloat(e.sRealType, 182);
                        record.disjunctratio = GetCommRealDataFloat(e.sRealType, 186);
                        record.delta = GetCommRealDataFloat(e.sRealType, 190);
                        record.gamma = GetCommRealDataFloat(e.sRealType, 191);
                        record.theta = GetCommRealDataFloat(e.sRealType, 193);
                        record.vega = GetCommRealDataFloat(e.sRealType, 192);
                        record.rho = GetCommRealDataFloat(e.sRealType, 194);
                        record.opened_agr_contpervday = GetCommRealDataLong(e.sRealType, 181);
                        record.contpervday_symbol = GetCommRealDataString(e.sRealType, 25);
                        record.prevtxamt_contrast_contract = GetCommRealDataFloat(e.sRealType, 26);
                        record.quoteprice_netspread = GetCommRealDataLong(e.sRealType, 137);
                        record.iv = GetCommRealDataFloat(e.sRealType, 187);
                        record.kospi200 = GetCommRealDataFloat(e.sRealType, 197);
                        record.open_opened_agramt = GetCommRealDataFloat(e.sRealType, 246);
                        record.max_opened_agramt = GetCommRealDataFloat(e.sRealType, 247);
                        record.min_opened_agramt = GetCommRealDataFloat(e.sRealType, 248);
                        record.future_curr_mnthitemindex = GetCommRealDataFloat(e.sRealType, 219);
                        record.opened_inde = GetCommRealDataLong(e.sRealType, 196);
                        record.timevalue = GetCommRealDataFloat(e.sRealType, 188);
                        record.ivv_iv = GetCommRealDataFloat(e.sRealType, 189);
                        record.prevtxamt_contrast_ratio = GetCommRealDataFloat(e.sRealType, 30);
                        record.spricediff_ohratio = GetCommRealDataFloat(e.sRealType, 391);
                        record.spricediff_hcratio = GetCommRealDataFloat(e.sRealType, 392);
                        record.spricediff_lcratio = GetCommRealDataFloat(e.sRealType, 393);
                        rtn = record; break;
                    }
                case "옵션호가잔량":
                    { TrOptionQuoteSpread record = new TrOptionQuoteSpread();

                        record.quotetime = GetCommRealDataString(e.sRealType, 21);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.bid1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.bid_amt1 = GetCommRealDataFloat(e.sRealType, 61);
                        record.bid_contrastprevtick1 = GetCommRealDataFloat(e.sRealType, 81);
                        record.bid_count1 = GetCommRealDataFloat(e.sRealType, 101);
                        record.ask1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.ask_amt1 = GetCommRealDataFloat(e.sRealType, 71);
                        record.ask_contrastprevtick1 = GetCommRealDataFloat(e.sRealType, 91);
                        record.ask_count1 = GetCommRealDataFloat(e.sRealType, 111);
                        record.bid2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.bid_amt2 = GetCommRealDataFloat(e.sRealType, 62);
                        record.bid_contrastprevtick2 = GetCommRealDataFloat(e.sRealType, 82);
                        record.bid_count2 = GetCommRealDataFloat(e.sRealType, 102);
                        record.ask2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.ask_amt2 = GetCommRealDataFloat(e.sRealType, 72);
                        record.ask_contrastprevtick2 = GetCommRealDataFloat(e.sRealType, 92);
                        record.ask_count2 = GetCommRealDataFloat(e.sRealType, 112);
                        record.bid3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.bid_amt3 = GetCommRealDataFloat(e.sRealType, 63);
                        record.bid_contrastprevtick3 = GetCommRealDataFloat(e.sRealType, 83);
                        record.bid_count3 = GetCommRealDataFloat(e.sRealType, 103);
                        record.ask3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.ask_amt3 = GetCommRealDataFloat(e.sRealType, 73);
                        record.ask_contrastprevtick3 = GetCommRealDataFloat(e.sRealType, 93);
                        record.ask_count3 = GetCommRealDataFloat(e.sRealType, 113);
                        record.bid4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.bid_amt4 = GetCommRealDataFloat(e.sRealType, 64);
                        record.bid_contrastprevtick4 = GetCommRealDataFloat(e.sRealType, 84);
                        record.bid_count4 = GetCommRealDataFloat(e.sRealType, 104);
                        record.ask4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.ask_amt4 = GetCommRealDataFloat(e.sRealType, 74);
                        record.ask_contrastprevtick4 = GetCommRealDataFloat(e.sRealType, 94);
                        record.ask_count4 = GetCommRealDataFloat(e.sRealType, 114);
                        record.bid5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.bid_amt5 = GetCommRealDataFloat(e.sRealType, 65);
                        record.bid_contrastprevtick5 = GetCommRealDataFloat(e.sRealType, 85);
                        record.bid_count5 = GetCommRealDataFloat(e.sRealType, 105);
                        record.ask5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.ask_amt5 = GetCommRealDataFloat(e.sRealType, 75);
                        record.ask_contrastprevtick5 = GetCommRealDataFloat(e.sRealType, 95);
                        record.ask_count5 = GetCommRealDataFloat(e.sRealType, 115);
                        record.bid_tredis = GetCommRealDataLong(e.sRealType, 121);
                        record.bid_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 122);
                        record.bid_tcount = GetCommRealDataLong(e.sRealType, 123);
                        record.ask_tredis = GetCommRealDataLong(e.sRealType, 125);
                        record.ask_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 126);
                        record.ask_tcount = GetCommRealDataLong(e.sRealType, 127);
                        record.quoteprice_netspread = GetCommRealDataLong(e.sRealType, 137);
                        record.netaskspread_taskresids_tbidresids = GetCommRealDataLong(e.sRealType, 128);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.estimcontractprice_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.estimcontractprice_prevdayclose_contrastsymbol = GetCommRealDataFloat(e.sRealType, 238);
                        record.estimcontractprice_prevdayclose_contrast = GetCommRealDataFloat(e.sRealType, 200);
                        record.estimcontractprice_prevdayclose_contrast_hlratio = GetCommRealDataFloat(e.sRealType, 201);
                        record.estimcontractprice = GetCommRealDataFloat(e.sRealType, 291);
                        record.estimcontractprice_contpervday_symbol = GetCommRealDataString(e.sRealType, 293);
                        record.estimcontractprice_contpervday = GetCommRealDataFloat(e.sRealType, 294);
                        record.estimcontractprice_contpervday_hlratio = GetCommRealDataFloat(e.sRealType, 295);
                        rtn = record; break;
                    }
                case "옵션이론가":
                    { TrOptionTheoPrice record = new TrOptionTheoPrice();

                        record.opened_agr = GetCommRealDataLong(e.sRealType, 195);
                        record.theoprice = GetCommRealDataFloat(e.sRealType, 182);
                        record.disjunctratio = GetCommRealDataFloat(e.sRealType, 186);
                        record.delta = GetCommRealDataFloat(e.sRealType, 190);
                        record.gamma = GetCommRealDataFloat(e.sRealType, 191);
                        record.theta = GetCommRealDataFloat(e.sRealType, 193);
                        record.vega = GetCommRealDataFloat(e.sRealType, 192);
                        record.rho = GetCommRealDataFloat(e.sRealType, 194);
                        record.opened_agr_contpervday = GetCommRealDataLong(e.sRealType, 181);
                        record.open_opened_agramt = GetCommRealDataFloat(e.sRealType, 246);
                        record.max_opened_agramt = GetCommRealDataFloat(e.sRealType, 247);
                        record.min_opened_agramt = GetCommRealDataFloat(e.sRealType, 248);
                        record.iv = GetCommRealDataFloat(e.sRealType, 187);
                        record.timevalue = GetCommRealDataFloat(e.sRealType, 188);
                        record.ivv_iv = GetCommRealDataFloat(e.sRealType, 189);
                        rtn = record; break;
                    }
                case "주식옵션시세":
                    { TrStockOptionPrice record = new TrStockOptionPrice();

                        record.ttime_hhmmss = GetCommRealDataString(e.sRealType, 20);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.prevday_contrast = GetCommRealDataFloat(e.sRealType, 11);
                        record.hlratio = GetCommRealDataFloat(e.sRealType, 12);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.tamount = GetCommRealDataLong(e.sRealType, 15);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.acctvalue = GetCommRealDataLong(e.sRealType, 14);
                        record.currprice = GetCommRealDataFloat(e.sRealType, 16);
                        record.high = GetCommRealDataFloat(e.sRealType, 17);
                        record.low = GetCommRealDataFloat(e.sRealType, 18);
                        record.opened_agr = GetCommRealDataLong(e.sRealType, 195);
                        record.theoprice = GetCommRealDataFloat(e.sRealType, 182);
                        record.disjunctratio = GetCommRealDataFloat(e.sRealType, 186);
                        record.delta = GetCommRealDataFloat(e.sRealType, 190);
                        record.gamma = GetCommRealDataFloat(e.sRealType, 191);
                        record.theta = GetCommRealDataFloat(e.sRealType, 193);
                        record.vega = GetCommRealDataFloat(e.sRealType, 192);
                        record.rho = GetCommRealDataFloat(e.sRealType, 194);
                        record.opened_agr_contpervday = GetCommRealDataLong(e.sRealType, 181);
                        record.contpervday_symbol = GetCommRealDataString(e.sRealType, 25);
                        record.prevtxamt_contrast_contract = GetCommRealDataFloat(e.sRealType, 26);
                        rtn = record; break;
                    }
                case "주식옵션호가잔량":
                    { TrStockOptionQuoteSpread record = new TrStockOptionQuoteSpread();

                        record.quotetime = GetCommRealDataString(e.sRealType, 21);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.bid1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.bid2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.bid3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.bid4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.bid5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.bid_amt1 = GetCommRealDataFloat(e.sRealType, 61);
                        record.bid_amt2 = GetCommRealDataFloat(e.sRealType, 62);
                        record.bid_amt3 = GetCommRealDataFloat(e.sRealType, 63);
                        record.bid_amt4 = GetCommRealDataFloat(e.sRealType, 64);
                        record.bid_amt5 = GetCommRealDataFloat(e.sRealType, 65);
                        record.bid_count1 = GetCommRealDataFloat(e.sRealType, 101);
                        record.bid_count2 = GetCommRealDataFloat(e.sRealType, 102);
                        record.bid_count3 = GetCommRealDataFloat(e.sRealType, 103);
                        record.bid_count4 = GetCommRealDataFloat(e.sRealType, 104);
                        record.bid_count5 = GetCommRealDataFloat(e.sRealType, 105);
                        record.ask1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.ask2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.ask3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.ask4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.ask5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.ask_amt1 = GetCommRealDataFloat(e.sRealType, 71);
                        record.ask_amt2 = GetCommRealDataFloat(e.sRealType, 72);
                        record.ask_amt3 = GetCommRealDataFloat(e.sRealType, 73);
                        record.ask_amt4 = GetCommRealDataFloat(e.sRealType, 74);
                        record.ask_amt5 = GetCommRealDataFloat(e.sRealType, 75);
                        record.ask_count1 = GetCommRealDataFloat(e.sRealType, 111);
                        record.ask_count2 = GetCommRealDataFloat(e.sRealType, 112);
                        record.ask_count3 = GetCommRealDataFloat(e.sRealType, 113);
                        record.ask_count4 = GetCommRealDataFloat(e.sRealType, 114);
                        record.ask_count5 = GetCommRealDataFloat(e.sRealType, 115);
                        record.bid_tredis = GetCommRealDataLong(e.sRealType, 121);
                        record.bid_tcount = GetCommRealDataLong(e.sRealType, 123);
                        record.ask_tredis = GetCommRealDataLong(e.sRealType, 125);
                        record.ask_tcount = GetCommRealDataLong(e.sRealType, 127);
                        record.estimcontractprice_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.estimcontractprice_prevdayclose_contrastsymbol = GetCommRealDataFloat(e.sRealType, 238);
                        record.estimcontractprice_prevdayclose_contrast = GetCommRealDataFloat(e.sRealType, 200);
                        record.estimcontractprice_prevdayclose_contrast_hlratio = GetCommRealDataFloat(e.sRealType, 201);
                        record.estimcontractprice = GetCommRealDataFloat(e.sRealType, 291);
                        record.estimcontractprice_contpervday_symbol = GetCommRealDataString(e.sRealType, 293);
                        record.estimcontractprice_contpervday = GetCommRealDataFloat(e.sRealType, 294);
                        record.estimcontractprice_contpervday_hlratio = GetCommRealDataFloat(e.sRealType, 295);
                        rtn = record; break;
                    }
                case "주식옵션이론가":
                    { TrStockOptionTheoPrice record = new TrStockOptionTheoPrice();

                        record.opened_agr = GetCommRealDataLong(e.sRealType, 195);
                        record.theoprice = GetCommRealDataFloat(e.sRealType, 182);
                        record.disjunctratio = GetCommRealDataFloat(e.sRealType, 186);
                        record.delta = GetCommRealDataFloat(e.sRealType, 190);
                        record.gamma = GetCommRealDataFloat(e.sRealType, 191);
                        record.theta = GetCommRealDataFloat(e.sRealType, 193);
                        record.vega = GetCommRealDataFloat(e.sRealType, 192);
                        record.rho = GetCommRealDataFloat(e.sRealType, 194);
                        record.opened_agr_contpervday = GetCommRealDataLong(e.sRealType, 181);
                        rtn = record; break;
                    }
                case "주식옵션우선호가":
                    { TrStockOptionPreferPrice record = new TrStockOptionPreferPrice();

                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        rtn = record; break;
                    }
                case "업종지수":
                    { TrIndustrialIndex record = new TrIndustrialIndex();

                        record.ttime_hhmmss = GetCommRealDataString(e.sRealType, 20);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.prevday_contrast = GetCommRealDataFloat(e.sRealType, 11);
                        record.hlratio = GetCommRealDataFloat(e.sRealType, 12);
                        record.tamount = GetCommRealDataLong(e.sRealType, 15);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.acctvalue = GetCommRealDataLong(e.sRealType, 14);
                        record.currprice = GetCommRealDataFloat(e.sRealType, 16);
                        record.high = GetCommRealDataFloat(e.sRealType, 17);
                        record.low = GetCommRealDataFloat(e.sRealType, 18);
                        record.contpervday_symbol = GetCommRealDataString(e.sRealType, 25);
                        record.prevtxamt_contrast_contract = GetCommRealDataFloat(e.sRealType, 26);
                        rtn = record; break;
                    }
                case "업종등락":
                    { TrIndustrialHighLowCount record = new TrIndustrialHighLowCount();

                        record.ttime_hhmmss = GetCommRealDataString(e.sRealType, 20);
                        record.bullcnt = GetCommRealDataInt(e.sRealType, 252);
                        record.highcnt = GetCommRealDataInt(e.sRealType, 251);
                        record.surgecnt = GetCommRealDataInt(e.sRealType, 253);
                        record.bearcnt = GetCommRealDataInt(e.sRealType, 255);
                        record.lowcnt = GetCommRealDataInt(e.sRealType, 254);
                        record.acctamt = GetCommRealDataFloat(e.sRealType, 13);
                        record.acctvalue = GetCommRealDataLong(e.sRealType, 14);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.prevday_contrast = GetCommRealDataFloat(e.sRealType, 11);
                        record.hlratio = GetCommRealDataFloat(e.sRealType, 12);
                        record.tx_stockcnt = GetCommRealDataInt(e.sRealType, 256);
                        record.tx_ratio = GetCommRealDataFloat(e.sRealType, 257);
                        record.contpervday_symbol = GetCommRealDataString(e.sRealType, 25);
                        rtn = record; break;
                    }
                case "장시작시간":
                    { TrMarketOpenTime record = new TrMarketOpenTime();

                        record.mmngttype = GetCommRealDataString(e.sRealType, 215);
                        record.ttime_hhmmss = GetCommRealDataString(e.sRealType, 20);
                        record.mopen_estimresidtime = GetCommRealDataString(e.sRealType, 214);
                        rtn = record; break;
                    }
                case "투자자ticker":
                    { TrTraderTicker record = new TrTraderTicker();

                        record.ptrader_ticker = GetCommRealDataString(e.sRealType, 216);
                        rtn = record; break;
                    }
                case "주문체결":
                    { TrOrderContract record = new TrOrderContract();

                        record.account = GetCommRealDataString(e.sRealType, 9201);
                        record.orderid = GetCommRealDataString(e.sRealType, 9203);
                        record.mngrid = GetCommRealDataString(e.sRealType, 9205);
                        record.stockid_indcode = GetCommRealDataString(e.sRealType, 9001);
                        record.orderoptype = GetCommRealDataString(e.sRealType, 912);
                        record.orderstat = GetCommRealDataString(e.sRealType, 913);
                        record.stockname = GetCommRealDataString(e.sRealType, 302);
                        record.orderamt = GetCommRealDataFloat(e.sRealType, 900);
                        record.orderprice = GetCommRealDataFloat(e.sRealType, 901);
                        record.openedamt = GetCommRealDataFloat(e.sRealType, 902);
                        record.accutvalue = GetCommRealDataLong(e.sRealType, 903);
                        record.orgordid = GetCommRealDataString(e.sRealType, 904);
                        record.ordertype = GetCommRealDataString(e.sRealType, 905);
                        record.ttype = GetCommRealDataString(e.sRealType, 906);
                        record.bidasktype = GetCommRealDataString(e.sRealType, 907);
                        record.order_ttime_hhmmssms = GetCommRealDataString(e.sRealType, 908);
                        record.tidt = GetCommRealDataString(e.sRealType, 909);
                        record.tprice = GetCommRealDataFloat(e.sRealType, 910);
                        record.tamt = GetCommRealDataFloat(e.sRealType, 911);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.unitprice = GetCommRealDataFloat(e.sRealType, 914);
                        record.unitamount = GetCommRealDataInt(e.sRealType, 915);
                        record.todaytx_comms = GetCommRealDataLong(e.sRealType, 938);
                        record.todaytxtax = GetCommRealDataLong(e.sRealType, 939);
                        rtn = record; break;
                    }
                case "잔고":
                    { TrTotalAccount record = new TrTotalAccount();

                        record.account = GetCommRealDataString(e.sRealType, 9201);
                        record.stockid_indcode = GetCommRealDataString(e.sRealType, 9001);
                        record.stockname = GetCommRealDataString(e.sRealType, 302);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.possquant = GetCommRealDataInt(e.sRealType, 930);
                        record.askprice = GetCommRealDataFloat(e.sRealType, 931);
                        record.taskprice = GetCommRealDataLong(e.sRealType, 932);
                        record.orderableamt = GetCommRealDataFloat(e.sRealType, 933);
                        record.todaytxamt = GetCommRealDataFloat(e.sRealType, 945);
                        record.bid_ask_type = GetCommRealDataString(e.sRealType, 946);
                        record.today_total_bid_pl = GetCommRealDataLong(e.sRealType, 950);
                        record.deposit = GetCommRealDataLong(e.sRealType, 951);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.sprice = GetCommRealDataFloat(e.sRealType, 307);
                        record.plratio = GetCommRealDataFloat(e.sRealType, 8019);
                        record.orderunit = GetCommRealDataString(e.sRealType, 397);
                        rtn = record; break;
                    }
                case "신용잔고":
                    { TrCreditAccount record = new TrCreditAccount();

                        record.account = GetCommRealDataString(e.sRealType, 9201);
                        record.stockid_indcode = GetCommRealDataString(e.sRealType, 9001);
                        record.credittype = GetCommRealDataString(e.sRealType, 917);
                        record.debtdate = GetCommRealDataString(e.sRealType, 916);
                        record.stockname = GetCommRealDataString(e.sRealType, 302);
                        record.currprice_currclose = GetCommRealDataFloat(e.sRealType, 10);
                        record.possquant = GetCommRealDataInt(e.sRealType, 930);
                        record.askprice = GetCommRealDataFloat(e.sRealType, 931);
                        record.taskprice = GetCommRealDataLong(e.sRealType, 932);
                        record.orderableamt = GetCommRealDataFloat(e.sRealType, 933);
                        record.todaytxamt = GetCommRealDataFloat(e.sRealType, 945);
                        record.bid_ask_type = GetCommRealDataString(e.sRealType, 946);
                        record.today_total_bid_pl = GetCommRealDataLong(e.sRealType, 950);
                        record.deposit = GetCommRealDataLong(e.sRealType, 951);
                        record._prefer_bid = GetCommRealDataFloat(e.sRealType, 27);
                        record._prefer_ask = GetCommRealDataFloat(e.sRealType, 28);
                        record.sprice = GetCommRealDataFloat(e.sRealType, 307);
                        record.plratio = GetCommRealDataFloat(e.sRealType, 8019);
                        record.creditamt = GetCommRealDataFloat(e.sRealType, 957);
                        record.creditint = GetCommRealDataFloat(e.sRealType, 958);
                        record.duedate = GetCommRealDataString(e.sRealType, 918);
                        record.todayrealizedpl_oilprice = GetCommRealDataLong(e.sRealType, 990);
                        record.todayrealizedplratio_oilprice = GetCommRealDataFloat(e.sRealType, 991);
                        record.todayrealizedpl_credit = GetCommRealDataLong(e.sRealType, 992);
                        record.todayrealizedplratio_credit = GetCommRealDataFloat(e.sRealType, 993);
                        record.mortloanamt = GetCommRealDataFloat(e.sRealType, 959);
                        rtn = record; break;
                    }
                case "주식시간외호가":
                    { TrAfterHour record = new TrAfterHour();

                        record.quotetime = GetCommRealDataString(e.sRealType, 21);
                        record.aftertime_bid_tredis = GetCommRealDataLong(e.sRealType, 131);
                        record.aftertime_bid_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 132);
                        record.aftertime_ask_tredis = GetCommRealDataLong(e.sRealType, 135);
                        record.aftertime_ask_tredis_contrastprevtick = GetCommRealDataLong(e.sRealType, 136);
                        rtn = record; break;
                    }
                case "주식당일거래원":
                    {
                        TrStockTraders record = new TrStockTraders();

                        record.bid_trader1 = GetCommRealDataString(e.sRealType, 141);
                        record.bid_trader_amt1 = GetCommRealDataFloat(e.sRealType, 161);
                        record.bid_traders_inde1 = GetCommRealDataLong(e.sRealType, 166);
                        record.bid_trader_code1 = GetCommRealDataString(e.sRealType, 146);
                        record.bid_trader_color1 = GetCommRealDataString(e.sRealType, 271);
                        record.ask_trader1 = GetCommRealDataString(e.sRealType, 151);
                        record.ask_trader_amt1 = GetCommRealDataFloat(e.sRealType, 171);
                        record.ask_traders_inde1 = GetCommRealDataLong(e.sRealType, 176);
                        record.ask_trader_code1 = GetCommRealDataString(e.sRealType, 156);
                        record.ask_trader_color1 = GetCommRealDataString(e.sRealType, 281);
                        record.bid_trader2 = GetCommRealDataString(e.sRealType, 142);
                        record.bid_trader_amt2 = GetCommRealDataFloat(e.sRealType, 162);
                        record.bid_traders_inde2 = GetCommRealDataLong(e.sRealType, 167);
                        record.bid_trader_code2 = GetCommRealDataString(e.sRealType, 147);
                        record.bid_trader_color2 = GetCommRealDataString(e.sRealType, 272);
                        record.ask_trader2 = GetCommRealDataString(e.sRealType, 152);
                        record.ask_trader_amt2 = GetCommRealDataFloat(e.sRealType, 172);
                        record.ask_traders_inde2 = GetCommRealDataLong(e.sRealType, 177);
                        record.ask_trader_code2 = GetCommRealDataString(e.sRealType, 157);
                        record.ask_trader_color2 = GetCommRealDataString(e.sRealType, 282);
                        record.bid_trader3 = GetCommRealDataString(e.sRealType, 143);
                        record.bid_trader_amt3 = GetCommRealDataFloat(e.sRealType, 163);
                        record.bid_traders_inde3 = GetCommRealDataLong(e.sRealType, 168);
                        record.bid_trader_code3 = GetCommRealDataString(e.sRealType, 148);
                        record.bid_trader_color3 = GetCommRealDataString(e.sRealType, 273);
                        record.ask_trader = GetCommRealDataString(e.sRealType, 153);
                        record.ask_trader_amt3 = GetCommRealDataFloat(e.sRealType, 173);
                        record.ask_traders_inde3 = GetCommRealDataLong(e.sRealType, 178);
                        record.ask_trader_code3 = GetCommRealDataString(e.sRealType, 158);
                        record.ask_trader_color3 = GetCommRealDataString(e.sRealType, 183);
                        record.bid_trader4 = GetCommRealDataString(e.sRealType, 144);
                        record.bid_trader_amt4 = GetCommRealDataFloat(e.sRealType, 164);
                        record.bid_traders_inde4 = GetCommRealDataLong(e.sRealType, 169);
                        record.bid_trader_code4 = GetCommRealDataString(e.sRealType, 149);
                        record.bid_trader_color4 = GetCommRealDataString(e.sRealType, 274);
                        record.ask_trader4 = GetCommRealDataString(e.sRealType, 154);
                        record.ask_trader_amt4 = GetCommRealDataFloat(e.sRealType, 174);
                        record.ask_traders_inde4 = GetCommRealDataLong(e.sRealType, 179);
                        record.ask_trader_code4 = GetCommRealDataString(e.sRealType, 159);
                        record.ask_trader_color4 = GetCommRealDataString(e.sRealType, 284);
                        record.bid_trader5 = GetCommRealDataString(e.sRealType, 145);
                        record.bid_trader_amt5 = GetCommRealDataFloat(e.sRealType, 165);
                        record.bid_traders_inde5 = GetCommRealDataLong(e.sRealType, 170);
                        record.bid_trader_code5 = GetCommRealDataString(e.sRealType, 150);
                        record.bid_trader_color5 = GetCommRealDataString(e.sRealType, 275);
                        record.ask_trader5 = GetCommRealDataString(e.sRealType, 155);
                        record.ask_trader_amt5 = GetCommRealDataFloat(e.sRealType, 175);
                        record.ask_traders_inde5 = GetCommRealDataLong(e.sRealType, 180);
                        record.ask_trader_code5 = GetCommRealDataString(e.sRealType, 160);
                        record.ask_trader_color5 = GetCommRealDataString(e.sRealType, 285);
                        record.foreign_estimbidamt = GetCommRealDataFloat(e.sRealType, 261);
                        record.foreign_estimbidamt_vlt = GetCommRealDataFloat(e.sRealType, 262);
                        record.foreign_estimaskamt = GetCommRealDataFloat(e.sRealType, 263);
                        record.foreign_estimaskamt_vlt = GetCommRealDataFloat(e.sRealType, 264);
                        record.foreign_estimasksum = GetCommRealDataLong(e.sRealType, 267);
                        record.foreign_netask_vlt = GetCommRealDataFloat(e.sRealType, 268);
                        record.mtype = GetCommRealDataString(e.sRealType, 337);

                        rtn = record;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if(rtn != null)
            {
                SerDeUtil.PublishRealTimeStructure(rtn);
                _rtmessageCounter++;
                if (OnRealTimeMessageCountModifiedHandler != null)
                    OnRealTimeMessageCountModifiedHandler(_rtmessageCounter);
            }

        }

        private void initialize(bool result)
        {
            if (!result)
            {
                Console.WriteLine("Connection Failed. After connection establishment, API module can retrieve account infomation from Kiwoom.");
                return;
            }
            // TODO :
        }

    }

    public class TrReceiver
    {

    }

}
