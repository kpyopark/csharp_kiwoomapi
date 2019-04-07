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
        private AxKHOpenAPI _openApi;
        public delegate void OnConnect(bool result);
        public OnConnect OnConnectEventHandler;

        public KiwoomAPI(AxKHOpenAPI openApi)
        {
            _openApi = openApi;
            _openApi.OnEventConnect += OnEventConnectEventHandler;
            _openApi.OnReceiveTrData += OnReceiveTrDataEventHandler;
            _openApi.OnReceiveRealData += OnReceiveRealDataEventHandler;

            OnConnectEventHandler = initialize;
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
            return Int32.Parse(_openApi.GetCommRealData(realType, fid));
        }

        private long GetCommRealDataLong(string realType, int fid)
        {
            return long.Parse(_openApi.GetCommRealData(realType, fid));
        }

        private float GetCommRealDataFloat(string realType, int fid)
        {
            return float.Parse(_openApi.GetCommRealData(realType, fid));
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
                    "000000", // REALTIME_SCREEN_NUMBER_LIST[cnt],
                    registeredTarget,
                    registeredFid,
                    (cnt == 0 ? "0" : "1"));
            }
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
                    { Tr주식시세 record = new Tr주식시세();

                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.전일_대비 = GetCommRealDataFloat(e.sRealType, 11);
                        record.등락율 = GetCommRealDataFloat(e.sRealType, 12);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.누적거래대금 = GetCommRealDataLong(e.sRealType, 14);
                        record.시가 = GetCommRealDataFloat(e.sRealType, 16);
                        record.고가 = GetCommRealDataFloat(e.sRealType, 17);
                        record.저가 = GetCommRealDataFloat(e.sRealType, 18);
                        record.전일대비_기호 = GetCommRealDataString(e.sRealType, 25);
                        record.전일거래량_대비_계약 = GetCommRealDataInt(e.sRealType, 26);
                        record.거래대금_증감 = GetCommRealDataFloat(e.sRealType, 29);
                        record.전일거래량_대비_비율_ = GetCommRealDataFloat(e.sRealType, 30);
                        record.거래회전율 = GetCommRealDataFloat(e.sRealType, 31);
                        record.거래비용 = GetCommRealDataLong(e.sRealType, 32);
                        record.시가총액_억_ = GetCommRealDataLong(e.sRealType, 311);
                        rtn = record; break;
                    }
                case "주식체결":
                    { Tr주식체결 record = new Tr주식체결();

                        record.체결시간__HHMMSS_ = GetCommRealDataString(e.sRealType, 20);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.전일_대비 = GetCommRealDataFloat(e.sRealType, 11);
                        record.등락율 = GetCommRealDataFloat(e.sRealType, 12);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.거래량 = GetCommRealDataLong(e.sRealType, 15);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.누적거래대금 = GetCommRealDataLong(e.sRealType, 14);
                        record.시가 = GetCommRealDataFloat(e.sRealType, 16);
                        record.고가 = GetCommRealDataFloat(e.sRealType, 17);
                        record.저가 = GetCommRealDataFloat(e.sRealType, 18);
                        record.전일대비_기호 = GetCommRealDataString(e.sRealType, 25);
                        record.전일거래량_대비_계약 = GetCommRealDataInt(e.sRealType, 26);
                        record.거래대금_증감 = GetCommRealDataFloat(e.sRealType, 29);
                        record.전일거래량_대비_비율_ = GetCommRealDataFloat(e.sRealType, 30);
                        record.거래회전율 = GetCommRealDataFloat(e.sRealType, 31);
                        record.거래비용 = GetCommRealDataLong(e.sRealType, 32);
                        record.체결강도 = GetCommRealDataLong(e.sRealType, 228);
                        record.시가총액_억_ = GetCommRealDataLong(e.sRealType, 311);
                        record.장구분 = GetCommRealDataString(e.sRealType, 290);
                        record.K_O_접근도__ELW조기종료발생_기준가격__지수_ = GetCommRealDataFloat(e.sRealType, 691);
                        rtn = record; break;
                    }
                case "주식우선호가":
                    { Tr주식우선호가 record = new Tr주식우선호가();

                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        rtn = record; break;
                    }
                case "주식호가잔량":
                    { Tr주식호가잔량 record = new Tr주식호가잔량();

                        record.호가시간 = GetCommRealDataString(e.sRealType, 21);
                        record.매도호가1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.매도호가_수량1 = GetCommRealDataInt(e.sRealType, 61);
                        record.매도호가_직전대비1 = GetCommRealDataFloat(e.sRealType, 81);
                        record.매수호가1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.매수호가_수량1 = GetCommRealDataInt(e.sRealType, 71);
                        record.매수호가_직전대비1 = GetCommRealDataFloat(e.sRealType, 91);
                        record.매도호가2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.매도호가_수량2 = GetCommRealDataInt(e.sRealType, 62);
                        record.매도호가_직전대비2 = GetCommRealDataFloat(e.sRealType, 82);
                        record.매수호가2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.매수호가_수량2 = GetCommRealDataInt(e.sRealType, 72);
                        record.매수호가_직전대비2 = GetCommRealDataFloat(e.sRealType, 92);
                        record.매도호가3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.매도호가_수량3 = GetCommRealDataInt(e.sRealType, 63);
                        record.매도호가_직전대비3 = GetCommRealDataFloat(e.sRealType, 83);
                        record.매수호가3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.매수호가_수량3 = GetCommRealDataInt(e.sRealType, 73);
                        record.매수호가_직전대비3 = GetCommRealDataFloat(e.sRealType, 93);
                        record.매도호가4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.매도호가_수량4 = GetCommRealDataInt(e.sRealType, 64);
                        record.매도호가_직전대비4 = GetCommRealDataFloat(e.sRealType, 84);
                        record.매수호가4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.매수호가_수량4 = GetCommRealDataInt(e.sRealType, 74);
                        record.매수호가_직전대비4 = GetCommRealDataFloat(e.sRealType, 94);
                        record.매도호가5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.매도호가_수량5 = GetCommRealDataInt(e.sRealType, 65);
                        record.매도호가_직전대비5 = GetCommRealDataFloat(e.sRealType, 85);
                        record.매수호가5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.매수호가_수량5 = GetCommRealDataInt(e.sRealType, 75);
                        record.매수호가_직전대비5 = GetCommRealDataFloat(e.sRealType, 95);
                        record.매도호가6 = GetCommRealDataFloat(e.sRealType, 46);
                        record.매도호가_수량6 = GetCommRealDataInt(e.sRealType, 66);
                        record.매도호가_직전대비6 = GetCommRealDataFloat(e.sRealType, 86);
                        record.매수호가6 = GetCommRealDataFloat(e.sRealType, 56);
                        record.매수호가_수량6 = GetCommRealDataInt(e.sRealType, 76);
                        record.매수호가_직전대비6 = GetCommRealDataFloat(e.sRealType, 96);
                        record.매도호가7 = GetCommRealDataFloat(e.sRealType, 47);
                        record.매도호가_수량7 = GetCommRealDataInt(e.sRealType, 67);
                        record.매도호가_직전대비7 = GetCommRealDataFloat(e.sRealType, 87);
                        record.매수호가7 = GetCommRealDataFloat(e.sRealType, 57);
                        record.매수호가_수량7 = GetCommRealDataInt(e.sRealType, 77);
                        record.매수호가_직전대비7 = GetCommRealDataFloat(e.sRealType, 97);
                        record.매도호가8 = GetCommRealDataFloat(e.sRealType, 48);
                        record.매도호가_수량8 = GetCommRealDataInt(e.sRealType, 68);
                        record.매도호가_직전대비8 = GetCommRealDataFloat(e.sRealType, 88);
                        record.매수호가8 = GetCommRealDataFloat(e.sRealType, 58);
                        record.매수호가_수량8 = GetCommRealDataInt(e.sRealType, 78);
                        record.매수호가_직전대비8 = GetCommRealDataFloat(e.sRealType, 98);
                        record.매도호가9 = GetCommRealDataFloat(e.sRealType, 49);
                        record.매도호가_수량9 = GetCommRealDataInt(e.sRealType, 69);
                        record.매도호가_직전대비9 = GetCommRealDataFloat(e.sRealType, 89);
                        record.매수호가9 = GetCommRealDataFloat(e.sRealType, 59);
                        record.매수호가_수량9 = GetCommRealDataInt(e.sRealType, 79);
                        record.매수호가_직전대비9 = GetCommRealDataFloat(e.sRealType, 99);
                        record.매도호가10 = GetCommRealDataFloat(e.sRealType, 50);
                        record.매도호가_수량10 = GetCommRealDataInt(e.sRealType, 70);
                        record.매도호가_직전대비10 = GetCommRealDataFloat(e.sRealType, 90);
                        record.매수호가10 = GetCommRealDataFloat(e.sRealType, 60);
                        record.매수호가_수량10 = GetCommRealDataInt(e.sRealType, 80);
                        record.매수호가_직전대비10 = GetCommRealDataFloat(e.sRealType, 100);
                        record.매도호가_총잔량 = GetCommRealDataLong(e.sRealType, 121);
                        record.매도호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 122);
                        record.매수호가_총잔량 = GetCommRealDataLong(e.sRealType, 125);
                        record.매수호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 126);
                        record.예상체결가_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.예상체결_수량 = GetCommRealDataLong(e.sRealType, 24);
                        record.순매수잔량_총매수잔량_총매도잔량_ = GetCommRealDataLong(e.sRealType, 128);
                        record.매수비율 = GetCommRealDataFloat(e.sRealType, 129);
                        record.순매도잔량_총매도잔량_총매수잔량_ = GetCommRealDataLong(e.sRealType, 138);
                        record.매도비율 = GetCommRealDataFloat(e.sRealType, 139);
                        record.예상체결가_전일종가_대비 = GetCommRealDataFloat(e.sRealType, 200);
                        record.예상체결가_전일종가_대비_등락율 = GetCommRealDataFloat(e.sRealType, 201);
                        record.예상체결가_전일종가_대비기호 = GetCommRealDataFloat(e.sRealType, 238);
                        record.예상체결가 = GetCommRealDataFloat(e.sRealType, 291);
                        record.예상체결량 = GetCommRealDataLong(e.sRealType, 292);
                        record.예상체결가_전일대비_기호 = GetCommRealDataString(e.sRealType, 293);
                        record.예상체결가_전일대비 = GetCommRealDataFloat(e.sRealType, 294);
                        record.예상체결가_전일대비_등락율 = GetCommRealDataFloat(e.sRealType, 295);
                        record.LP매도호가_수량1 = GetCommRealDataInt(e.sRealType, 621);
                        record.LP매수호가_수량1 = GetCommRealDataInt(e.sRealType, 631);
                        record.LP매도호가_수량2 = GetCommRealDataInt(e.sRealType, 622);
                        record.LP매수호가_수량2 = GetCommRealDataInt(e.sRealType, 632);
                        record.LP매도호가_수량3 = GetCommRealDataInt(e.sRealType, 623);
                        record.LP매수호가_수량3 = GetCommRealDataInt(e.sRealType, 633);
                        record.LP매도호가_수량4 = GetCommRealDataInt(e.sRealType, 624);
                        record.LP매수호가_수량4 = GetCommRealDataInt(e.sRealType, 634);
                        record.LP매도호가_수량5 = GetCommRealDataInt(e.sRealType, 625);
                        record.LP매수호가_수량5 = GetCommRealDataInt(e.sRealType, 635);
                        record.LP매도호가_수량6 = GetCommRealDataInt(e.sRealType, 626);
                        record.LP매수호가_수량6 = GetCommRealDataInt(e.sRealType, 636);
                        record.LP매도호가_수량7 = GetCommRealDataInt(e.sRealType, 627);
                        record.LP매수호가_수량7 = GetCommRealDataInt(e.sRealType, 637);
                        record.LP매도호가_수량8 = GetCommRealDataInt(e.sRealType, 628);
                        record.LP매수호가_수량8 = GetCommRealDataInt(e.sRealType, 638);
                        record.LP매도호가_수량9 = GetCommRealDataInt(e.sRealType, 629);
                        record.LP매수호가_수량9 = GetCommRealDataInt(e.sRealType, 639);
                        record.LP매도호가_수량10 = GetCommRealDataInt(e.sRealType, 630);
                        record.LP매수호가_수량10 = GetCommRealDataInt(e.sRealType, 640);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.전일거래량대비예상체결률 = GetCommRealDataFloat(e.sRealType, 299);
                        record.장운영구분 = GetCommRealDataString(e.sRealType, 215);
                        record.투자자별_ticker = GetCommRealDataString(e.sRealType, 216);
                        rtn = record; break;
                    }
                case "선물시세":
                    { Tr선물시세 record = new Tr선물시세();

                        record.체결시간__HHMMSS_ = GetCommRealDataString(e.sRealType, 20);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.전일_대비 = GetCommRealDataFloat(e.sRealType, 11);
                        record.등락율 = GetCommRealDataFloat(e.sRealType, 12);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.거래량 = GetCommRealDataLong(e.sRealType, 15);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.누적거래대금 = GetCommRealDataLong(e.sRealType, 14);
                        record.시가 = GetCommRealDataFloat(e.sRealType, 16);
                        record.고가 = GetCommRealDataFloat(e.sRealType, 17);
                        record.저가 = GetCommRealDataFloat(e.sRealType, 18);
                        record.미결제_약정 = GetCommRealDataLong(e.sRealType, 195);
                        record.이론가 = GetCommRealDataFloat(e.sRealType, 182);
                        record.이론베이시스 = GetCommRealDataFloat(e.sRealType, 184);
                        record.매수_거래원_색깔3 = GetCommRealDataString(e.sRealType, 183);
                        record.괴리율 = GetCommRealDataFloat(e.sRealType, 186);
                        record.미결제_약정_전일대비 = GetCommRealDataLong(e.sRealType, 181);
                        record.괴리도 = GetCommRealDataFloat(e.sRealType, 185);
                        record.전일대비_기호 = GetCommRealDataString(e.sRealType, 25);
                        record.KOSPI200 = GetCommRealDataFloat(e.sRealType, 197);
                        record.전일거래량_대비_계약 = GetCommRealDataInt(e.sRealType, 26);
                        record.시초_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 246);
                        record.초고_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 247);
                        record.최저_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 248);
                        record.전일거래량_대비_비율_ = GetCommRealDataFloat(e.sRealType, 30);
                        record.미결제_증감 = GetCommRealDataLong(e.sRealType, 196);
                        rtn = record; break;
                    }
                case "선물호가잔량":
                    { Tr선물호가잔량 record = new Tr선물호가잔량();

                        record.호가시간 = GetCommRealDataString(e.sRealType, 21);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.매도호가1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.매도호가_수량1 = GetCommRealDataInt(e.sRealType, 61);
                        record.매도호가_직전대비1 = GetCommRealDataFloat(e.sRealType, 81);
                        record.매도호가_건수1 = GetCommRealDataFloat(e.sRealType, 101);
                        record.매수호가1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.매수호가_수량1 = GetCommRealDataInt(e.sRealType, 71);
                        record.매수호가_직전대비1 = GetCommRealDataFloat(e.sRealType, 91);
                        record.매수호가_건수1 = GetCommRealDataFloat(e.sRealType, 111);
                        record.매도호가2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.매도호가_수량2 = GetCommRealDataInt(e.sRealType, 62);
                        record.매도호가_직전대비2 = GetCommRealDataFloat(e.sRealType, 82);
                        record.매도호가_건수2 = GetCommRealDataFloat(e.sRealType, 102);
                        record.매수호가2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.매수호가_수량2 = GetCommRealDataInt(e.sRealType, 72);
                        record.매수호가_직전대비2 = GetCommRealDataFloat(e.sRealType, 92);
                        record.매수호가_건수2 = GetCommRealDataFloat(e.sRealType, 112);
                        record.매도호가3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.매도호가_수량3 = GetCommRealDataInt(e.sRealType, 63);
                        record.매도호가_직전대비3 = GetCommRealDataFloat(e.sRealType, 83);
                        record.매도호가_건수3 = GetCommRealDataFloat(e.sRealType, 103);
                        record.매수호가3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.매수호가_수량3 = GetCommRealDataInt(e.sRealType, 73);
                        record.매수호가_직전대비3 = GetCommRealDataFloat(e.sRealType, 93);
                        record.매수호가_건수3 = GetCommRealDataFloat(e.sRealType, 113);
                        record.매도호가4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.매도호가_수량4 = GetCommRealDataInt(e.sRealType, 64);
                        record.매도호가_직전대비4 = GetCommRealDataFloat(e.sRealType, 84);
                        record.매도호가_건수4 = GetCommRealDataFloat(e.sRealType, 104);
                        record.매수호가4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.매수호가_수량4 = GetCommRealDataInt(e.sRealType, 74);
                        record.매수호가_직전대비4 = GetCommRealDataFloat(e.sRealType, 94);
                        record.매수호가_건수4 = GetCommRealDataFloat(e.sRealType, 114);
                        record.매도호가5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.매도호가_수량5 = GetCommRealDataInt(e.sRealType, 65);
                        record.매도호가_직전대비5 = GetCommRealDataFloat(e.sRealType, 85);
                        record.매도호가_건수5 = GetCommRealDataFloat(e.sRealType, 105);
                        record.매수호가5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.매수호가_수량5 = GetCommRealDataInt(e.sRealType, 75);
                        record.매수호가_직전대비5 = GetCommRealDataFloat(e.sRealType, 95);
                        record.매수호가_건수5 = GetCommRealDataFloat(e.sRealType, 115);
                        record.매도호가_총잔량 = GetCommRealDataLong(e.sRealType, 121);
                        record.매도호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 122);
                        record.매도호가_총_건수 = GetCommRealDataLong(e.sRealType, 123);
                        record.매수호가_총잔량 = GetCommRealDataLong(e.sRealType, 125);
                        record.매수호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 126);
                        record.매수호가_총_건수 = GetCommRealDataLong(e.sRealType, 127);
                        record.호가_순잔량 = GetCommRealDataLong(e.sRealType, 137);
                        record.순매수잔량_총매수잔량_총매도잔량_ = GetCommRealDataLong(e.sRealType, 128);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.예상체결가_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.예상체결가_전일종가_대비기호 = GetCommRealDataFloat(e.sRealType, 238);
                        record.예상체결가_전일종가_대비 = GetCommRealDataFloat(e.sRealType, 200);
                        record.예상체결가_전일종가_대비_등락율 = GetCommRealDataFloat(e.sRealType, 201);
                        record.예상체결가 = GetCommRealDataFloat(e.sRealType, 291);
                        record.예상체결가_전일대비_기호 = GetCommRealDataString(e.sRealType, 293);
                        record.예상체결가_전일대비 = GetCommRealDataFloat(e.sRealType, 294);
                        record.예상체결가_전일대비_등락율 = GetCommRealDataFloat(e.sRealType, 295);
                        rtn = record; break;
                    }
                case "선물이론가":
                    { Tr선물이론가 record = new Tr선물이론가();

                        record.미결제_약정 = GetCommRealDataLong(e.sRealType, 195);
                        record.이론가 = GetCommRealDataFloat(e.sRealType, 182);
                        record.이론베이시스 = GetCommRealDataFloat(e.sRealType, 184);
                        record.매수_거래원_색깔3 = GetCommRealDataString(e.sRealType, 183);
                        record.괴리율 = GetCommRealDataFloat(e.sRealType, 186);
                        record.미결제_약정_전일대비 = GetCommRealDataLong(e.sRealType, 181);
                        record.괴리도 = GetCommRealDataFloat(e.sRealType, 185);
                        record.시초_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 246);
                        record.초고_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 247);
                        record.최저_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 248);
                        rtn = record; break;
                    }
                case "옵션시세":
                    { Tr옵션시세 record = new Tr옵션시세();

                        record.체결시간__HHMMSS_ = GetCommRealDataString(e.sRealType, 20);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.전일_대비 = GetCommRealDataFloat(e.sRealType, 11);
                        record.등락율 = GetCommRealDataFloat(e.sRealType, 12);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.거래량 = GetCommRealDataLong(e.sRealType, 15);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.누적거래대금 = GetCommRealDataLong(e.sRealType, 14);
                        record.시가 = GetCommRealDataFloat(e.sRealType, 16);
                        record.고가 = GetCommRealDataFloat(e.sRealType, 17);
                        record.저가 = GetCommRealDataFloat(e.sRealType, 18);
                        record.미결제_약정 = GetCommRealDataLong(e.sRealType, 195);
                        record.이론가 = GetCommRealDataFloat(e.sRealType, 182);
                        record.괴리율 = GetCommRealDataFloat(e.sRealType, 186);
                        record.델타 = GetCommRealDataFloat(e.sRealType, 190);
                        record.감마 = GetCommRealDataFloat(e.sRealType, 191);
                        record.세타 = GetCommRealDataFloat(e.sRealType, 193);
                        record.베가 = GetCommRealDataFloat(e.sRealType, 192);
                        record.로 = GetCommRealDataFloat(e.sRealType, 194);
                        record.미결제_약정_전일대비 = GetCommRealDataLong(e.sRealType, 181);
                        record.전일대비_기호 = GetCommRealDataString(e.sRealType, 25);
                        record.전일거래량_대비_계약 = GetCommRealDataInt(e.sRealType, 26);
                        record.호가_순잔량 = GetCommRealDataLong(e.sRealType, 137);
                        record.내재가치 = GetCommRealDataFloat(e.sRealType, 187);
                        record.KOSPI200 = GetCommRealDataFloat(e.sRealType, 197);
                        record.시초_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 246);
                        record.초고_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 247);
                        record.최저_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 248);
                        record.선물_최근_월물지수 = GetCommRealDataFloat(e.sRealType, 219);
                        record.미결제_증감 = GetCommRealDataLong(e.sRealType, 196);
                        record.시간가치 = GetCommRealDataFloat(e.sRealType, 188);
                        record.내재변동성_I_V__ = GetCommRealDataFloat(e.sRealType, 189);
                        record.전일거래량_대비_비율_ = GetCommRealDataFloat(e.sRealType, 30);
                        record.기준가대비_시고등락율 = GetCommRealDataFloat(e.sRealType, 391);
                        record.기준가대비_고가등락율 = GetCommRealDataFloat(e.sRealType, 392);
                        record.기준가대비_저가등락율 = GetCommRealDataFloat(e.sRealType, 393);
                        rtn = record; break;
                    }
                case "옵션호가잔량":
                    { Tr옵션호가잔량 record = new Tr옵션호가잔량();

                        record.호가시간 = GetCommRealDataString(e.sRealType, 21);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.매도호가1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.매도호가_수량1 = GetCommRealDataInt(e.sRealType, 61);
                        record.매도호가_직전대비1 = GetCommRealDataFloat(e.sRealType, 81);
                        record.매도호가_건수1 = GetCommRealDataFloat(e.sRealType, 101);
                        record.매수호가1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.매수호가_수량1 = GetCommRealDataInt(e.sRealType, 71);
                        record.매수호가_직전대비1 = GetCommRealDataFloat(e.sRealType, 91);
                        record.매수호가_건수1 = GetCommRealDataFloat(e.sRealType, 111);
                        record.매도호가2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.매도호가_수량2 = GetCommRealDataInt(e.sRealType, 62);
                        record.매도호가_직전대비2 = GetCommRealDataFloat(e.sRealType, 82);
                        record.매도호가_건수2 = GetCommRealDataFloat(e.sRealType, 102);
                        record.매수호가2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.매수호가_수량2 = GetCommRealDataInt(e.sRealType, 72);
                        record.매수호가_직전대비2 = GetCommRealDataFloat(e.sRealType, 92);
                        record.매수호가_건수2 = GetCommRealDataFloat(e.sRealType, 112);
                        record.매도호가3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.매도호가_수량3 = GetCommRealDataInt(e.sRealType, 63);
                        record.매도호가_직전대비3 = GetCommRealDataFloat(e.sRealType, 83);
                        record.매도호가_건수3 = GetCommRealDataFloat(e.sRealType, 103);
                        record.매수호가3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.매수호가_수량3 = GetCommRealDataInt(e.sRealType, 73);
                        record.매수호가_직전대비3 = GetCommRealDataFloat(e.sRealType, 93);
                        record.매수호가_건수3 = GetCommRealDataFloat(e.sRealType, 113);
                        record.매도호가4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.매도호가_수량4 = GetCommRealDataInt(e.sRealType, 64);
                        record.매도호가_직전대비4 = GetCommRealDataFloat(e.sRealType, 84);
                        record.매도호가_건수4 = GetCommRealDataFloat(e.sRealType, 104);
                        record.매수호가4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.매수호가_수량4 = GetCommRealDataInt(e.sRealType, 74);
                        record.매수호가_직전대비4 = GetCommRealDataFloat(e.sRealType, 94);
                        record.매수호가_건수4 = GetCommRealDataFloat(e.sRealType, 114);
                        record.매도호가5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.매도호가_수량5 = GetCommRealDataInt(e.sRealType, 65);
                        record.매도호가_직전대비5 = GetCommRealDataFloat(e.sRealType, 85);
                        record.매도호가_건수5 = GetCommRealDataFloat(e.sRealType, 105);
                        record.매수호가5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.매수호가_수량5 = GetCommRealDataInt(e.sRealType, 75);
                        record.매수호가_직전대비5 = GetCommRealDataFloat(e.sRealType, 95);
                        record.매수호가_건수5 = GetCommRealDataFloat(e.sRealType, 115);
                        record.매도호가_총잔량 = GetCommRealDataLong(e.sRealType, 121);
                        record.매도호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 122);
                        record.매도호가_총_건수 = GetCommRealDataLong(e.sRealType, 123);
                        record.매수호가_총잔량 = GetCommRealDataLong(e.sRealType, 125);
                        record.매수호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 126);
                        record.매수호가_총_건수 = GetCommRealDataLong(e.sRealType, 127);
                        record.호가_순잔량 = GetCommRealDataLong(e.sRealType, 137);
                        record.순매수잔량_총매수잔량_총매도잔량_ = GetCommRealDataLong(e.sRealType, 128);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.예상체결가_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.예상체결가_전일종가_대비기호 = GetCommRealDataFloat(e.sRealType, 238);
                        record.예상체결가_전일종가_대비 = GetCommRealDataFloat(e.sRealType, 200);
                        record.예상체결가_전일종가_대비_등락율 = GetCommRealDataFloat(e.sRealType, 201);
                        record.예상체결가 = GetCommRealDataFloat(e.sRealType, 291);
                        record.예상체결가_전일대비_기호 = GetCommRealDataString(e.sRealType, 293);
                        record.예상체결가_전일대비 = GetCommRealDataFloat(e.sRealType, 294);
                        record.예상체결가_전일대비_등락율 = GetCommRealDataFloat(e.sRealType, 295);
                        rtn = record; break;
                    }
                case "옵션이론가":
                    { Tr옵션이론가 record = new Tr옵션이론가();

                        record.미결제_약정 = GetCommRealDataLong(e.sRealType, 195);
                        record.이론가 = GetCommRealDataFloat(e.sRealType, 182);
                        record.괴리율 = GetCommRealDataFloat(e.sRealType, 186);
                        record.델타 = GetCommRealDataFloat(e.sRealType, 190);
                        record.감마 = GetCommRealDataFloat(e.sRealType, 191);
                        record.세타 = GetCommRealDataFloat(e.sRealType, 193);
                        record.베가 = GetCommRealDataFloat(e.sRealType, 192);
                        record.로 = GetCommRealDataFloat(e.sRealType, 194);
                        record.미결제_약정_전일대비 = GetCommRealDataLong(e.sRealType, 181);
                        record.시초_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 246);
                        record.초고_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 247);
                        record.최저_미결제_약정수량 = GetCommRealDataLong(e.sRealType, 248);
                        record.내재가치 = GetCommRealDataFloat(e.sRealType, 187);
                        record.시간가치 = GetCommRealDataFloat(e.sRealType, 188);
                        record.내재변동성_I_V__ = GetCommRealDataFloat(e.sRealType, 189);
                        rtn = record; break;
                    }
                case "주식옵션시세":
                    { Tr주식옵션시세 record = new Tr주식옵션시세();

                        record.체결시간__HHMMSS_ = GetCommRealDataString(e.sRealType, 20);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.전일_대비 = GetCommRealDataFloat(e.sRealType, 11);
                        record.등락율 = GetCommRealDataFloat(e.sRealType, 12);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.거래량 = GetCommRealDataLong(e.sRealType, 15);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.누적거래대금 = GetCommRealDataLong(e.sRealType, 14);
                        record.시가 = GetCommRealDataFloat(e.sRealType, 16);
                        record.고가 = GetCommRealDataFloat(e.sRealType, 17);
                        record.저가 = GetCommRealDataFloat(e.sRealType, 18);
                        record.미결제_약정 = GetCommRealDataLong(e.sRealType, 195);
                        record.이론가 = GetCommRealDataFloat(e.sRealType, 182);
                        record.괴리율 = GetCommRealDataFloat(e.sRealType, 186);
                        record.델타 = GetCommRealDataFloat(e.sRealType, 190);
                        record.감마 = GetCommRealDataFloat(e.sRealType, 191);
                        record.세타 = GetCommRealDataFloat(e.sRealType, 193);
                        record.베가 = GetCommRealDataFloat(e.sRealType, 192);
                        record.로 = GetCommRealDataFloat(e.sRealType, 194);
                        record.미결제_약정_전일대비 = GetCommRealDataLong(e.sRealType, 181);
                        record.전일대비_기호 = GetCommRealDataString(e.sRealType, 25);
                        record.전일거래량_대비_계약 = GetCommRealDataInt(e.sRealType, 26);
                        rtn = record; break;
                    }
                case "주식옵션호가잔량":
                    { Tr주식옵션호가잔량 record = new Tr주식옵션호가잔량();

                        record.호가시간 = GetCommRealDataString(e.sRealType, 21);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.매도호가1 = GetCommRealDataFloat(e.sRealType, 41);
                        record.매도호가2 = GetCommRealDataFloat(e.sRealType, 42);
                        record.매도호가3 = GetCommRealDataFloat(e.sRealType, 43);
                        record.매도호가4 = GetCommRealDataFloat(e.sRealType, 44);
                        record.매도호가5 = GetCommRealDataFloat(e.sRealType, 45);
                        record.매도호가_수량1 = GetCommRealDataInt(e.sRealType, 61);
                        record.매도호가_수량2 = GetCommRealDataInt(e.sRealType, 62);
                        record.매도호가_수량3 = GetCommRealDataInt(e.sRealType, 63);
                        record.매도호가_수량4 = GetCommRealDataInt(e.sRealType, 64);
                        record.매도호가_수량5 = GetCommRealDataInt(e.sRealType, 65);
                        record.매도호가_건수1 = GetCommRealDataFloat(e.sRealType, 101);
                        record.매도호가_건수2 = GetCommRealDataFloat(e.sRealType, 102);
                        record.매도호가_건수3 = GetCommRealDataFloat(e.sRealType, 103);
                        record.매도호가_건수4 = GetCommRealDataFloat(e.sRealType, 104);
                        record.매도호가_건수5 = GetCommRealDataFloat(e.sRealType, 105);
                        record.매수호가1 = GetCommRealDataFloat(e.sRealType, 51);
                        record.매수호가2 = GetCommRealDataFloat(e.sRealType, 52);
                        record.매수호가3 = GetCommRealDataFloat(e.sRealType, 53);
                        record.매수호가4 = GetCommRealDataFloat(e.sRealType, 54);
                        record.매수호가5 = GetCommRealDataFloat(e.sRealType, 55);
                        record.매수호가_수량1 = GetCommRealDataInt(e.sRealType, 71);
                        record.매수호가_수량2 = GetCommRealDataInt(e.sRealType, 72);
                        record.매수호가_수량3 = GetCommRealDataInt(e.sRealType, 73);
                        record.매수호가_수량4 = GetCommRealDataInt(e.sRealType, 74);
                        record.매수호가_수량5 = GetCommRealDataInt(e.sRealType, 75);
                        record.매수호가_건수1 = GetCommRealDataFloat(e.sRealType, 111);
                        record.매수호가_건수2 = GetCommRealDataFloat(e.sRealType, 112);
                        record.매수호가_건수3 = GetCommRealDataFloat(e.sRealType, 113);
                        record.매수호가_건수4 = GetCommRealDataFloat(e.sRealType, 114);
                        record.매수호가_건수5 = GetCommRealDataFloat(e.sRealType, 115);
                        record.매도호가_총잔량 = GetCommRealDataLong(e.sRealType, 121);
                        record.매도호가_총_건수 = GetCommRealDataLong(e.sRealType, 123);
                        record.매수호가_총잔량 = GetCommRealDataLong(e.sRealType, 125);
                        record.매수호가_총_건수 = GetCommRealDataLong(e.sRealType, 127);
                        record.예상체결가_ = GetCommRealDataFloat(e.sRealType, 23);
                        record.예상체결가_전일종가_대비기호 = GetCommRealDataFloat(e.sRealType, 238);
                        record.예상체결가_전일종가_대비 = GetCommRealDataFloat(e.sRealType, 200);
                        record.예상체결가_전일종가_대비_등락율 = GetCommRealDataFloat(e.sRealType, 201);
                        record.예상체결가 = GetCommRealDataFloat(e.sRealType, 291);
                        record.예상체결가_전일대비_기호 = GetCommRealDataString(e.sRealType, 293);
                        record.예상체결가_전일대비 = GetCommRealDataFloat(e.sRealType, 294);
                        record.예상체결가_전일대비_등락율 = GetCommRealDataFloat(e.sRealType, 295);
                        rtn = record; break;
                    }
                case "주식옵션이론가":
                    { Tr주식옵션이론가 record = new Tr주식옵션이론가();

                        record.미결제_약정 = GetCommRealDataLong(e.sRealType, 195);
                        record.이론가 = GetCommRealDataFloat(e.sRealType, 182);
                        record.괴리율 = GetCommRealDataFloat(e.sRealType, 186);
                        record.델타 = GetCommRealDataFloat(e.sRealType, 190);
                        record.감마 = GetCommRealDataFloat(e.sRealType, 191);
                        record.세타 = GetCommRealDataFloat(e.sRealType, 193);
                        record.베가 = GetCommRealDataFloat(e.sRealType, 192);
                        record.로 = GetCommRealDataFloat(e.sRealType, 194);
                        record.미결제_약정_전일대비 = GetCommRealDataLong(e.sRealType, 181);
                        rtn = record; break;
                    }
                case "주식옵션우선호가":
                    { Tr주식옵션우선호가 record = new Tr주식옵션우선호가();

                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        rtn = record; break;
                    }
                case "업종지수":
                    { Tr업종지수 record = new Tr업종지수();

                        record.체결시간__HHMMSS_ = GetCommRealDataString(e.sRealType, 20);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.전일_대비 = GetCommRealDataFloat(e.sRealType, 11);
                        record.등락율 = GetCommRealDataFloat(e.sRealType, 12);
                        record.거래량 = GetCommRealDataLong(e.sRealType, 15);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.누적거래대금 = GetCommRealDataLong(e.sRealType, 14);
                        record.시가 = GetCommRealDataFloat(e.sRealType, 16);
                        record.고가 = GetCommRealDataFloat(e.sRealType, 17);
                        record.저가 = GetCommRealDataFloat(e.sRealType, 18);
                        record.전일대비_기호 = GetCommRealDataString(e.sRealType, 25);
                        record.전일거래량_대비_계약 = GetCommRealDataInt(e.sRealType, 26);
                        rtn = record; break;
                    }
                case "업종등락":
                    { Tr업종등락 record = new Tr업종등락();

                        record.체결시간__HHMMSS_ = GetCommRealDataString(e.sRealType, 20);
                        record.상승종목수 = GetCommRealDataInt(e.sRealType, 252);
                        record.상한종목수 = GetCommRealDataInt(e.sRealType, 251);
                        record.보합종목수 = GetCommRealDataInt(e.sRealType, 253);
                        record.하락종목수 = GetCommRealDataInt(e.sRealType, 255);
                        record.하한종목수 = GetCommRealDataInt(e.sRealType, 254);
                        record.누적거래량 = GetCommRealDataLong(e.sRealType, 13);
                        record.누적거래대금 = GetCommRealDataLong(e.sRealType, 14);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.전일_대비 = GetCommRealDataFloat(e.sRealType, 11);
                        record.등락율 = GetCommRealDataFloat(e.sRealType, 12);
                        record.거래형성_종목수 = GetCommRealDataInt(e.sRealType, 256);
                        record.거래형성_비율 = GetCommRealDataFloat(e.sRealType, 257);
                        record.전일대비_기호 = GetCommRealDataString(e.sRealType, 25);
                        rtn = record; break;
                    }
                case "장시작시간":
                    { Tr장시작시간 record = new Tr장시작시간();

                        record.장운영구분 = GetCommRealDataString(e.sRealType, 215);
                        record.체결시간__HHMMSS_ = GetCommRealDataString(e.sRealType, 20);
                        record.장시작_예상잔여시간 = GetCommRealDataString(e.sRealType, 214);
                        rtn = record; break;
                    }
                case "투자자ticker":
                    { Tr투자자ticker record = new Tr투자자ticker();

                        record.투자자별_ticker = GetCommRealDataString(e.sRealType, 216);
                        rtn = record; break;
                    }
                case "주문체결":
                    { Tr주문체결 record = new Tr주문체결();

                        record.계좌번호 = GetCommRealDataString(e.sRealType, 9201);
                        record.주문번호 = GetCommRealDataString(e.sRealType, 9203);
                        record.관리자사번 = GetCommRealDataString(e.sRealType, 9205);
                        record.종목코드__업종코드 = GetCommRealDataString(e.sRealType, 9001);
                        record.주문업무분류 = GetCommRealDataString(e.sRealType, 912);
                        record.주문상태_접수__확인__체결_ = GetCommRealDataString(e.sRealType, 913);
                        record.종목명 = GetCommRealDataString(e.sRealType, 302);
                        record.주문수량 = GetCommRealDataInt(e.sRealType, 900);
                        record.주문가격 = GetCommRealDataFloat(e.sRealType, 901);
                        record.미체결수량 = GetCommRealDataInt(e.sRealType, 902);
                        record.체결누계금액 = GetCommRealDataLong(e.sRealType, 903);
                        record.원주문번호 = GetCommRealDataString(e.sRealType, 904);
                        record.주문구분 = GetCommRealDataString(e.sRealType, 905);
                        record.매매구분 = GetCommRealDataString(e.sRealType, 906);
                        record.매도수구분__1_매도_2_매수_ = GetCommRealDataString(e.sRealType, 907);
                        record.주문_체결시간_HHMMSSMS_ = GetCommRealDataString(e.sRealType, 908);
                        record.체결번호 = GetCommRealDataString(e.sRealType, 909);
                        record.체결가 = GetCommRealDataFloat(e.sRealType, 910);
                        record.체결량 = GetCommRealDataInt(e.sRealType, 911);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.단위체결가 = GetCommRealDataFloat(e.sRealType, 914);
                        record.단위체결량 = GetCommRealDataInt(e.sRealType, 915);
                        record.당일매매_수수료 = GetCommRealDataLong(e.sRealType, 938);
                        record.당일매매세금 = GetCommRealDataLong(e.sRealType, 939);
                        rtn = record; break;
                    }
                case "잔고":
                    { Tr잔고 record = new Tr잔고();

                        record.계좌번호 = GetCommRealDataString(e.sRealType, 9201);
                        record.종목코드__업종코드 = GetCommRealDataString(e.sRealType, 9001);
                        record.종목명 = GetCommRealDataString(e.sRealType, 302);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.보유수량 = GetCommRealDataInt(e.sRealType, 930);
                        record.매입단가 = GetCommRealDataFloat(e.sRealType, 931);
                        record.총매입가 = GetCommRealDataLong(e.sRealType, 932);
                        record.주문가능수량 = GetCommRealDataInt(e.sRealType, 933);
                        record.당일순매수량 = GetCommRealDataLong(e.sRealType, 945);
                        record.매도_매수_구분 = GetCommRealDataString(e.sRealType, 946);
                        record.당일_총_매도_손익 = GetCommRealDataLong(e.sRealType, 950);
                        record.예수금 = GetCommRealDataLong(e.sRealType, 951);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.기준가 = GetCommRealDataFloat(e.sRealType, 307);
                        record.손익율 = GetCommRealDataFloat(e.sRealType, 8019);
                        record.주식옵션거래단위 = GetCommRealDataString(e.sRealType, 397);
                        rtn = record; break;
                    }
                case "신용잔고":
                    { Tr신용잔고 record = new Tr신용잔고();

                        record.계좌번호 = GetCommRealDataString(e.sRealType, 9201);
                        record.종목코드__업종코드 = GetCommRealDataString(e.sRealType, 9001);
                        record.신용구분 = GetCommRealDataString(e.sRealType, 917);
                        record.대출일 = GetCommRealDataString(e.sRealType, 916);
                        record.종목명 = GetCommRealDataString(e.sRealType, 302);
                        record.현재가__실시간종가 = GetCommRealDataFloat(e.sRealType, 10);
                        record.보유수량 = GetCommRealDataInt(e.sRealType, 930);
                        record.매입단가 = GetCommRealDataFloat(e.sRealType, 931);
                        record.총매입가 = GetCommRealDataLong(e.sRealType, 932);
                        record.주문가능수량 = GetCommRealDataInt(e.sRealType, 933);
                        record.당일순매수량 = GetCommRealDataLong(e.sRealType, 945);
                        record.매도_매수_구분 = GetCommRealDataString(e.sRealType, 946);
                        record.당일_총_매도_손익 = GetCommRealDataLong(e.sRealType, 950);
                        record.예수금 = GetCommRealDataLong(e.sRealType, 951);
                        record._최우선_매도호가 = GetCommRealDataFloat(e.sRealType, 27);
                        record._최우선_매수호가 = GetCommRealDataFloat(e.sRealType, 28);
                        record.기준가 = GetCommRealDataFloat(e.sRealType, 307);
                        record.손익율 = GetCommRealDataFloat(e.sRealType, 8019);
                        record.신용금액 = GetCommRealDataLong(e.sRealType, 957);
                        record.신용이자 = GetCommRealDataFloat(e.sRealType, 958);
                        record.만기일 = GetCommRealDataString(e.sRealType, 918);
                        record.당일실현손익_유가_ = GetCommRealDataLong(e.sRealType, 990);
                        record.당일실현손익률_유가_ = GetCommRealDataFloat(e.sRealType, 991);
                        record.당일실현손익_신용_ = GetCommRealDataLong(e.sRealType, 992);
                        record.당일실현손익률_신용_ = GetCommRealDataFloat(e.sRealType, 993);
                        record.담보대출수량 = GetCommRealDataLong(e.sRealType, 959);
                        rtn = record; break;
                    }
                case "주식시간외호가":
                    { Tr주식시간외호가 record = new Tr주식시간외호가();

                        record.호가시간 = GetCommRealDataString(e.sRealType, 21);
                        record.시간외_매도호가_총잔량 = GetCommRealDataLong(e.sRealType, 131);
                        record.시간외_매도호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 132);
                        record.시간외_매수호가_총잔량 = GetCommRealDataLong(e.sRealType, 135);
                        record.시간외_매수호가_총잔량_직전대비 = GetCommRealDataLong(e.sRealType, 136);
                        rtn = record; break;
                    }
                case "주식당일거래원":
                    {
                        Tr주식당일거래원 record = new Tr주식당일거래원();

                        record.매도_거래원1 = GetCommRealDataString(e.sRealType, 141);
                        record.매도_거래원_수량1 = GetCommRealDataLong(e.sRealType, 161);
                        record.매도_거래원별_증감1 = GetCommRealDataLong(e.sRealType, 166);
                        record.매도_거래원_코드1 = GetCommRealDataString(e.sRealType, 146);
                        record.매도_거래원_색깔1 = GetCommRealDataString(e.sRealType, 271);
                        record.매수_거래원1 = GetCommRealDataString(e.sRealType, 151);
                        record.매수_거래원_수량1 = GetCommRealDataLong(e.sRealType, 171);
                        record.매수_거래원별_증감1 = GetCommRealDataLong(e.sRealType, 176);
                        record.매수_거래원_코드1 = GetCommRealDataString(e.sRealType, 156);
                        record.매수_거래원_색깔1 = GetCommRealDataString(e.sRealType, 281);
                        record.매도_거래원2 = GetCommRealDataString(e.sRealType, 142);
                        record.매도_거래원_수량2 = GetCommRealDataLong(e.sRealType, 162);
                        record.매도_거래원별_증감2 = GetCommRealDataLong(e.sRealType, 167);
                        record.매도_거래원_코드2 = GetCommRealDataString(e.sRealType, 147);
                        record.매도_거래원_색깔2 = GetCommRealDataString(e.sRealType, 272);
                        record.매수_거래원2 = GetCommRealDataString(e.sRealType, 152);
                        record.매수_거래원_수량2 = GetCommRealDataLong(e.sRealType, 172);
                        record.매수_거래원별_증감2 = GetCommRealDataLong(e.sRealType, 177);
                        record.매수_거래원_코드2 = GetCommRealDataString(e.sRealType, 157);
                        record.매수_거래원_색깔2 = GetCommRealDataString(e.sRealType, 282);
                        record.매도_거래원3 = GetCommRealDataString(e.sRealType, 143);
                        record.매도_거래원_수량3 = GetCommRealDataLong(e.sRealType, 163);
                        record.매도_거래원별_증감3 = GetCommRealDataLong(e.sRealType, 168);
                        record.매도_거래원_코드3 = GetCommRealDataString(e.sRealType, 148);
                        record.매도_거래원_색깔3 = GetCommRealDataString(e.sRealType, 273);
                        record.매수_거래원 = GetCommRealDataString(e.sRealType, 153);
                        record.매수_거래원_수량3 = GetCommRealDataLong(e.sRealType, 173);
                        record.매수_거래원별_증감3 = GetCommRealDataLong(e.sRealType, 178);
                        record.매수_거래원_코드3 = GetCommRealDataString(e.sRealType, 158);
                        record.매수_거래원_색깔3 = GetCommRealDataString(e.sRealType, 183);
                        record.매도_거래원4 = GetCommRealDataString(e.sRealType, 144);
                        record.매도_거래원_수량4 = GetCommRealDataLong(e.sRealType, 164);
                        record.매도_거래원별_증감4 = GetCommRealDataLong(e.sRealType, 169);
                        record.매도_거래원_코드4 = GetCommRealDataString(e.sRealType, 149);
                        record.매도_거래원_색깔4 = GetCommRealDataString(e.sRealType, 274);
                        record.매수_거래원4 = GetCommRealDataString(e.sRealType, 154);
                        record.매수_거래원_수량4 = GetCommRealDataLong(e.sRealType, 174);
                        record.매수_거래원별_증감4 = GetCommRealDataLong(e.sRealType, 179);
                        record.매수_거래원_코드4 = GetCommRealDataString(e.sRealType, 159);
                        record.매수_거래원_색깔4 = GetCommRealDataString(e.sRealType, 284);
                        record.매도_거래원5 = GetCommRealDataString(e.sRealType, 145);
                        record.매도_거래원_수량5 = GetCommRealDataLong(e.sRealType, 165);
                        record.매도_거래원별_증감5 = GetCommRealDataLong(e.sRealType, 170);
                        record.매도_거래원_코드5 = GetCommRealDataString(e.sRealType, 150);
                        record.매도_거래원_색깔5 = GetCommRealDataString(e.sRealType, 275);
                        record.매수_거래원5 = GetCommRealDataString(e.sRealType, 155);
                        record.매수_거래원_수량5 = GetCommRealDataLong(e.sRealType, 175);
                        record.매수_거래원별_증감5 = GetCommRealDataLong(e.sRealType, 180);
                        record.매수_거래원_코드5 = GetCommRealDataString(e.sRealType, 160);
                        record.매수_거래원_색깔5 = GetCommRealDataString(e.sRealType, 285);
                        record.외국계_매도추정합 = GetCommRealDataLong(e.sRealType, 261);
                        record.외국계_매도추정합_변동 = GetCommRealDataFloat(e.sRealType, 262);
                        record.외국계_매수추정합 = GetCommRealDataLong(e.sRealType, 263);
                        record.외국계_매수추정합_변동 = GetCommRealDataFloat(e.sRealType, 264);
                        record.외국계_순매수추정합 = GetCommRealDataLong(e.sRealType, 267);
                        record.외국계_순매수_변동 = GetCommRealDataFloat(e.sRealType, 268);
                        record.거래소구분 = GetCommRealDataString(e.sRealType, 337);

                        rtn = record;
                        break;
                    }
                default:
                    break;
            }

            if(rtn != null)
            {
                SerDeUtil.PublishRealTimeStructure(rtn);
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
