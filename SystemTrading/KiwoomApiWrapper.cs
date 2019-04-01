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

    public class BaseStructure
    {

    }

    public class TrStructure : BaseStructure
    {

    }

    public class Tr주식시세 : TrStructure
    {

        float 현재가__실시간종가;
        float 전일_대비;
        float 등락율;
        float _최우선_매도호가;
        float _최우선_매수호가;
        long 누적거래량;
        long 누적거래대금;
        float 시가;
        float 고가;
        float 저가;
        string 전일대비_기호;
        int 전일거래량_대비_계약;
        float 거래대금_증감;
        float 전일거래량_대비_비율_;
        float 거래회전율;
        long 거래비용;
        long 시가총액_억_;
    }
    public class Tr주식체결 : TrStructure
    {

        string 체결시간__HHMMSS_;
        float 현재가__실시간종가;
        float 전일_대비;
        float 등락율;
        float _최우선_매도호가;
        float _최우선_매수호가;
        long 거래량;
        long 누적거래량;
        long 누적거래대금;
        float 시가;
        float 고가;
        float 저가;
        string 전일대비_기호;
        int 전일거래량_대비_계약;
        float 거래대금_증감;
        float 전일거래량_대비_비율_;
        float 거래회전율;
        long 거래비용;
        long 체결강도;
        long 시가총액_억_;
        string 장구분;
        float K_O_접근도__ELW조기종료발생_기준가격__지수_;
    }
    public class Tr주식우선호가 : TrStructure
    {

        float _최우선_매도호가;
        float _최우선_매수호가;
    }
    public class Tr주식호가잔량 : TrStructure
    {

        string 호가시간;
        float 매도호가1;
        int 매도호가_수량1;
        float 매도호가_직전대비1;
        float 매수호가1;
        int 매수호가_수량1;
        float 매수호가_직전대비1;
        float 매도호가2;
        int 매도호가_수량2;
        float 매도호가_직전대비2;
        float 매수호가2;
        int 매수호가_수량2;
        float 매수호가_직전대비2;
        float 매도호가3;
        int 매도호가_수량3;
        float 매도호가_직전대비3;
        float 매수호가3;
        int 매수호가_수량3;
        float 매수호가_직전대비3;
        float 매도호가4;
        int 매도호가_수량4;
        float 매도호가_직전대비4;
        float 매수호가4;
        int 매수호가_수량4;
        float 매수호가_직전대비4;
        float 매도호가5;
        int 매도호가_수량5;
        float 매도호가_직전대비5;
        float 매수호가5;
        int 매수호가_수량5;
        float 매수호가_직전대비5;
        float 매도호가6;
        int 매도호가_수량6;
        float 매도호가_직전대비6;
        float 매수호가6;
        int 매수호가_수량6;
        float 매수호가_직전대비6;
        float 매도호가7;
        int 매도호가_수량7;
        float 매도호가_직전대비7;
        float 매수호가7;
        int 매수호가_수량7;
        float 매수호가_직전대비7;
        float 매도호가8;
        int 매도호가_수량8;
        float 매도호가_직전대비8;
        float 매수호가8;
        int 매수호가_수량8;
        float 매수호가_직전대비8;
        float 매도호가9;
        int 매도호가_수량9;
        float 매도호가_직전대비9;
        float 매수호가9;
        int 매수호가_수량9;
        float 매수호가_직전대비9;
        float 매도호가10;
        int 매도호가_수량10;
        float 매도호가_직전대비10;
        float 매수호가10;
        int 매수호가_수량10;
        float 매수호가_직전대비10;
        long 매도호가_총잔량;
        long 매도호가_총잔량_직전대비;
        long 매수호가_총잔량;
        long 매수호가_총잔량_직전대비;
        float 예상체결가_;
        long 예상체결_수량;
        long 순매수잔량_총매수잔량_총매도잔량_;
        float 매수비율;
        long 순매도잔량_총매도잔량_총매수잔량_;
        float 매도비율;
        float 예상체결가_전일종가_대비;
        float 예상체결가_전일종가_대비_등락율;
        float 예상체결가_전일종가_대비기호;
        float 예상체결가;
        long 예상체결량;
        string 예상체결가_전일대비_기호;
        float 예상체결가_전일대비;
        float 예상체결가_전일대비_등락율;
        int LP매도호가_수량1;
        int LP매수호가_수량1;
        int LP매도호가_수량2;
        int LP매수호가_수량2;
        int LP매도호가_수량3;
        int LP매수호가_수량3;
        int LP매도호가_수량4;
        int LP매수호가_수량4;
        int LP매도호가_수량5;
        int LP매수호가_수량5;
        int LP매도호가_수량6;
        int LP매수호가_수량6;
        int LP매도호가_수량7;
        int LP매수호가_수량7;
        int LP매도호가_수량8;
        int LP매수호가_수량8;
        int LP매도호가_수량9;
        int LP매수호가_수량9;
        int LP매도호가_수량10;
        int LP매수호가_수량10;
        long 누적거래량;
        float 전일거래량대비예상체결률;
        string 장운영구분;
        string 투자자별_ticker;
    }
    public class Tr선물시세 : TrStructure
    {

        string 체결시간__HHMMSS_;
        float 현재가__실시간종가;
        float 전일_대비;
        float 등락율;
        float _최우선_매도호가;
        float _최우선_매수호가;
        long 거래량;
        long 누적거래량;
        long 누적거래대금;
        float 시가;
        float 고가;
        float 저가;
        long 미결제_약정;
        float 이론가;
        float 이론베이시스;
        string 매수_거래원_색깔3;
        float 괴리율;
        long 미결제_약정_전일대비;
        float 괴리도;
        string 전일대비_기호;
        float KOSPI200;
        int 전일거래량_대비_계약;
        long 시초_미결제_약정수량;
        long 초고_미결제_약정수량;
        long 최저_미결제_약정수량;
        float 전일거래량_대비_비율_;
        long 미결제_증감;
    }
    public class Tr선물호가잔량 : TrStructure
    {

        string 호가시간;
        float _최우선_매도호가;
        float _최우선_매수호가;
        float 매도호가1;
        int 매도호가_수량1;
        float 매도호가_직전대비1;
        float 매도호가_건수1;
        float 매수호가1;
        int 매수호가_수량1;
        float 매수호가_직전대비1;
        float 매수호가_건수1;
        float 매도호가2;
        int 매도호가_수량2;
        float 매도호가_직전대비2;
        float 매도호가_건수2;
        float 매수호가2;
        int 매수호가_수량2;
        float 매수호가_직전대비2;
        float 매수호가_건수2;
        float 매도호가3;
        int 매도호가_수량3;
        float 매도호가_직전대비3;
        float 매도호가_건수3;
        float 매수호가3;
        int 매수호가_수량3;
        float 매수호가_직전대비3;
        float 매수호가_건수3;
        float 매도호가4;
        int 매도호가_수량4;
        float 매도호가_직전대비4;
        float 매도호가_건수4;
        float 매수호가4;
        int 매수호가_수량4;
        float 매수호가_직전대비4;
        float 매수호가_건수4;
        float 매도호가5;
        int 매도호가_수량5;
        float 매도호가_직전대비5;
        float 매도호가_건수5;
        float 매수호가5;
        int 매수호가_수량5;
        float 매수호가_직전대비5;
        float 매수호가_건수5;
        long 매도호가_총잔량;
        long 매도호가_총잔량_직전대비;
        long 매도호가_총_건수;
        long 매수호가_총잔량;
        long 매수호가_총잔량_직전대비;
        long 매수호가_총_건수;
        long 호가_순잔량;
        long 순매수잔량_총매수잔량_총매도잔량_;
        long 누적거래량;
        float 예상체결가_;
        float 예상체결가_전일종가_대비기호;
        float 예상체결가_전일종가_대비;
        float 예상체결가_전일종가_대비_등락율;
        float 예상체결가;
        string 예상체결가_전일대비_기호;
        float 예상체결가_전일대비;
        float 예상체결가_전일대비_등락율;
    }
    public class Tr선물이론가 : TrStructure
    {

        long 미결제_약정;
        float 이론가;
        float 이론베이시스;
        string 매수_거래원_색깔3;
        float 괴리율;
        long 미결제_약정_전일대비;
        float 괴리도;
        long 시초_미결제_약정수량;
        long 초고_미결제_약정수량;
        long 최저_미결제_약정수량;
    }
    public class Tr옵션시세 : TrStructure
    {

        string 체결시간__HHMMSS_;
        float 현재가__실시간종가;
        float 전일_대비;
        float 등락율;
        float _최우선_매도호가;
        float _최우선_매수호가;
        long 거래량;
        long 누적거래량;
        long 누적거래대금;
        float 시가;
        float 고가;
        float 저가;
        long 미결제_약정;
        float 이론가;
        float 괴리율;
        float 델타;
        float 감마;
        float 세타;
        float 베가;
        float 로;
        long 미결제_약정_전일대비;
        string 전일대비_기호;
        int 전일거래량_대비_계약;
        long 호가_순잔량;
        float 내재가치;
        float KOSPI200;
        long 시초_미결제_약정수량;
        long 초고_미결제_약정수량;
        long 최저_미결제_약정수량;
        float 선물_최근_월물지수;
        long 미결제_증감;
        float 시간가치;
        float 내재변동성_I_V__;
        float 전일거래량_대비_비율_;
        float 기준가대비_시고등락율;
        float 기준가대비_고가등락율;
        float 기준가대비_저가등락율;
    }
    public class Tr옵션호가잔량 : TrStructure
    {

        string 호가시간;
        float _최우선_매도호가;
        float _최우선_매수호가;
        float 매도호가1;
        int 매도호가_수량1;
        float 매도호가_직전대비1;
        float 매도호가_건수1;
        float 매수호가1;
        int 매수호가_수량1;
        float 매수호가_직전대비1;
        float 매수호가_건수1;
        float 매도호가2;
        int 매도호가_수량2;
        float 매도호가_직전대비2;
        float 매도호가_건수2;
        float 매수호가2;
        int 매수호가_수량2;
        float 매수호가_직전대비2;
        float 매수호가_건수2;
        float 매도호가3;
        int 매도호가_수량3;
        float 매도호가_직전대비3;
        float 매도호가_건수3;
        float 매수호가3;
        int 매수호가_수량3;
        float 매수호가_직전대비3;
        float 매수호가_건수3;
        float 매도호가4;
        int 매도호가_수량4;
        float 매도호가_직전대비4;
        float 매도호가_건수4;
        float 매수호가4;
        int 매수호가_수량4;
        float 매수호가_직전대비4;
        float 매수호가_건수4;
        float 매도호가5;
        int 매도호가_수량5;
        float 매도호가_직전대비5;
        float 매도호가_건수5;
        float 매수호가5;
        int 매수호가_수량5;
        float 매수호가_직전대비5;
        float 매수호가_건수5;
        long 매도호가_총잔량;
        long 매도호가_총잔량_직전대비;
        long 매도호가_총_건수;
        long 매수호가_총잔량;
        long 매수호가_총잔량_직전대비;
        long 매수호가_총_건수;
        long 호가_순잔량;
        long 순매수잔량_총매수잔량_총매도잔량_;
        long 누적거래량;
        float 예상체결가_;
        float 예상체결가_전일종가_대비기호;
        float 예상체결가_전일종가_대비;
        float 예상체결가_전일종가_대비_등락율;
        float 예상체결가;
        string 예상체결가_전일대비_기호;
        float 예상체결가_전일대비;
        float 예상체결가_전일대비_등락율;
    }
    public class Tr옵션이론가 : TrStructure
    {

        long 미결제_약정;
        float 이론가;
        float 괴리율;
        float 델타;
        float 감마;
        float 세타;
        float 베가;
        float 로;
        long 미결제_약정_전일대비;
        long 시초_미결제_약정수량;
        long 초고_미결제_약정수량;
        long 최저_미결제_약정수량;
        float 내재가치;
        float 시간가치;
        float 내재변동성_I_V__;
    }
    public class Tr주식옵션시세 : TrStructure
    {

        string 체결시간__HHMMSS_;
        float 현재가__실시간종가;
        float 전일_대비;
        float 등락율;
        float _최우선_매도호가;
        float _최우선_매수호가;
        long 거래량;
        long 누적거래량;
        long 누적거래대금;
        float 시가;
        float 고가;
        float 저가;
        long 미결제_약정;
        float 이론가;
        float 괴리율;
        float 델타;
        float 감마;
        float 세타;
        float 베가;
        float 로;
        long 미결제_약정_전일대비;
        string 전일대비_기호;
        int 전일거래량_대비_계약;
    }
    public class Tr주식옵션호가잔량 : TrStructure
    {

        string 호가시간;
        float _최우선_매도호가;
        float _최우선_매수호가;
        float 매도호가1;
        float 매도호가2;
        float 매도호가3;
        float 매도호가4;
        float 매도호가5;
        int 매도호가_수량1;
        int 매도호가_수량2;
        int 매도호가_수량3;
        int 매도호가_수량4;
        int 매도호가_수량5;
        float 매도호가_건수1;
        float 매도호가_건수2;
        float 매도호가_건수3;
        float 매도호가_건수4;
        float 매도호가_건수5;
        float 매수호가1;
        float 매수호가2;
        float 매수호가3;
        float 매수호가4;
        float 매수호가5;
        int 매수호가_수량1;
        int 매수호가_수량2;
        int 매수호가_수량3;
        int 매수호가_수량4;
        int 매수호가_수량5;
        float 매수호가_건수1;
        float 매수호가_건수2;
        float 매수호가_건수3;
        float 매수호가_건수4;
        float 매수호가_건수5;
        long 매도호가_총잔량;
        long 매도호가_총_건수;
        long 매수호가_총잔량;
        long 매수호가_총_건수;
        float 예상체결가_;
        float 예상체결가_전일종가_대비기호;
        float 예상체결가_전일종가_대비;
        float 예상체결가_전일종가_대비_등락율;
        float 예상체결가;
        string 예상체결가_전일대비_기호;
        float 예상체결가_전일대비;
        float 예상체결가_전일대비_등락율;
    }
    public class Tr주식옵션이론가 : TrStructure
    {

        long 미결제_약정;
        float 이론가;
        float 괴리율;
        float 델타;
        float 감마;
        float 세타;
        float 베가;
        float 로;
        long 미결제_약정_전일대비;
    }
    public class Tr주식옵션우선호가 : TrStructure
    {

        float 현재가__실시간종가;
        float _최우선_매도호가;
        float _최우선_매수호가;
    }
    public class Tr업종지수 : TrStructure
    {

        string 체결시간__HHMMSS_;
        float 현재가__실시간종가;
        float 전일_대비;
        float 등락율;
        long 거래량;
        long 누적거래량;
        long 누적거래대금;
        float 시가;
        float 고가;
        float 저가;
        string 전일대비_기호;
        int 전일거래량_대비_계약;
    }
    public class Tr업종등락 : TrStructure
    {

        string 체결시간__HHMMSS_;
        int 상승종목수;
        int 상한종목수;
        int 보합종목수;
        int 하락종목수;
        int 하한종목수;
        long 누적거래량;
        long 누적거래대금;
        float 현재가__실시간종가;
        float 전일_대비;
        float 등락율;
        int 거래형성_종목수;
        float 거래형성_비율;
        string 전일대비_기호;
    }
    public class Tr장시작시간 : TrStructure
    {

        string 장운영구분;
        string 체결시간__HHMMSS_;
        string 장시작_예상잔여시간;
    }
    public class Tr투자자ticker : TrStructure
    {
        string 투자자별_ticker;
    }
    public class Tr주문체결 : TrStructure
    {

        string 계좌번호;
        string 주문번호;
        string 관리자사번;
        string 종목코드__업종코드;
        string 주문업무분류;
        string 주문상태_접수__확인__체결_;
        string 종목명;
        int 주문수량;
        float 주문가격;
        int 미체결수량;
        long 체결누계금액;
        string 원주문번호;
        string 주문구분;
        string 매매구분;
        string 매도수구분__1_매도_2_매수_;
        string 주문_체결시간_HHMMSSMS_;
        string 체결번호;
        float 체결가;
        int 체결량;
        float 현재가__실시간종가;
        float _최우선_매도호가;
        float _최우선_매수호가;
        float 단위체결가;
        int 단위체결량;
        long 당일매매_수수료;
        long 당일매매세금;
    }
    public class Tr잔고 : TrStructure
    {

        string 계좌번호;
        string 종목코드__업종코드;
        string 종목명;
        float 현재가__실시간종가;
        int 보유수량;
        float 매입단가;
        long 총매입가;
        int 주문가능수량;
        long 당일순매수량;
        string 매도_매수_구분;
        long 당일_총_매도_손익;
        long 예수금;
        float _최우선_매도호가;
        float _최우선_매수호가;
        float 기준가;
        float 손익율;
        string 주식옵션거래단위;
    }
    public class Tr신용잔고 : TrStructure
    {

        string 계좌번호;
        string 종목코드__업종코드;
        string 신용구분;
        string 대출일;
        string 종목명;
        float 현재가__실시간종가;
        int 보유수량;
        float 매입단가;
        long 총매입가;
        int 주문가능수량;
        long 당일순매수량;
        string 매도_매수_구분;
        long 당일_총_매도_손익;
        long 예수금;
        float _최우선_매도호가;
        float _최우선_매수호가;
        float 기준가;
        float 손익율;
        long 신용금액;
        float 신용이자;
        string 만기일;
        long 당일실현손익_유가_;
        float 당일실현손익률_유가_;
        long 당일실현손익_신용_;
        float 당일실현손익률_신용_;
        long 담보대출수량;
    }
    public class Tr주식시간외호가 : TrStructure
    {

        string 호가시간;
        long 시간외_매도호가_총잔량;
        long 시간외_매도호가_총잔량_직전대비;
        long 시간외_매수호가_총잔량;
        long 시간외_매수호가_총잔량_직전대비;
    }
    public class Tr주식당일거래원 : TrStructure
    {

        string 매도_거래원1;
        long 매도_거래원_수량1;
        long 매도_거래원별_증감1;
        string 매도_거래원_코드1;
        string 매도_거래원_색깔1;
        string 매수_거래원1;
        long 매수_거래원_수량1;
        long 매수_거래원별_증감1;
        string 매수_거래원_코드1;
        string 매수_거래원_색깔1;
        string 매도_거래원2;
        long 매도_거래원_수량2;
        long 매도_거래원별_증감2;
        string 매도_거래원_코드2;
        string 매도_거래원_색깔2;
        string 매수_거래원2;
        long 매수_거래원_수량2;
        long 매수_거래원별_증감2;
        string 매수_거래원_코드2;
        string 매수_거래원_색깔2;
        string 매도_거래원3;
        long 매도_거래원_수량3;
        long 매도_거래원별_증감3;
        string 매도_거래원_코드3;
        string 매도_거래원_색깔3;
        string 매수_거래원;
        long 매수_거래원_수량3;
        long 매수_거래원별_증감3;
        string 매수_거래원_코드3;
        string 매수_거래원_색깔3;
        string 매도_거래원4;
        long 매도_거래원_수량4;
        long 매도_거래원별_증감4;
        string 매도_거래원_코드4;
        string 매도_거래원_색깔4;
        string 매수_거래원4;
        long 매수_거래원_수량4;
        long 매수_거래원별_증감4;
        string 매수_거래원_코드4;
        string 매수_거래원_색깔4;
        string 매도_거래원5;
        long 매도_거래원_수량5;
        long 매도_거래원별_증감5;
        string 매도_거래원_코드5;
        string 매도_거래원_색깔5;
        string 매수_거래원5;
        long 매수_거래원_수량5;
        long 매수_거래원별_증감5;
        string 매수_거래원_코드5;
        string 매수_거래원_색깔5;
        long 외국계_매도추정합;
        float 외국계_매도추정합_변동;
        long 외국계_매수추정합;
        float 외국계_매수추정합_변동;
        long 외국계_순매수추정합;
        float 외국계_순매수_변동;
        string 거래소구분;
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
            _openApi.OnReceiveTrData += OnReceiveTrDataEventHandler;
            OnConnectEventHandler = initialize;
        }

        public int CommConnect()
        {
            return _openApi.CommConnect();
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

        private void OnReceiveTrDataEventHandler(object sender, _DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            
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
