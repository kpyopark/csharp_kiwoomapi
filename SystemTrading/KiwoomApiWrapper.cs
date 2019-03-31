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

    public static class KiwoomUtil
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return null; // could also return string.Empty
        }
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
            OnConnectEventHandler = initialize;
        }

        public int CommConnect()
        {
            return _openApi.CommConnect();
        }

        private void OnEventConnectEventHandler(object sender, _DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            switch((KiwoomError)e.nErrCode)
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

}
