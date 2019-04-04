using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;

namespace SystemTrading
{
    class KiwoomTrStructure
    {
    }

    public class BaseStructure
    {

    }

    public class TrStructure : BaseStructure
    {

    }

    public class Tr주식시세 : TrStructure
    {

        public float 현재가__실시간종가;
        public float 전일_대비;
        public float 등락율;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public long 누적거래량;
        public long 누적거래대금;
        public float 시가;
        public float 고가;
        public float 저가;
        public string 전일대비_기호;
        public int 전일거래량_대비_계약;
        public float 거래대금_증감;
        public float 전일거래량_대비_비율_;
        public float 거래회전율;
        public long 거래비용;
        public long 시가총액_억_;
    }
    public class Tr주식체결 : TrStructure
    {

        public string 체결시간__HHMMSS_;
        public float 현재가__실시간종가;
        public float 전일_대비;
        public float 등락율;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public long 거래량;
        public long 누적거래량;
        public long 누적거래대금;
        public float 시가;
        public float 고가;
        public float 저가;
        public string 전일대비_기호;
        public int 전일거래량_대비_계약;
        public float 거래대금_증감;
        public float 전일거래량_대비_비율_;
        public float 거래회전율;
        public long 거래비용;
        public long 체결강도;
        public long 시가총액_억_;
        public string 장구분;
        public float K_O_접근도__ELW조기종료발생_기준가격__지수_;
    }
    public class Tr주식우선호가 : TrStructure
    {

        public float _최우선_매도호가;
        public float _최우선_매수호가;
    }
    public class Tr주식호가잔량 : TrStructure
    {

        public string 호가시간;
        public float 매도호가1;
        public int 매도호가_수량1;
        public float 매도호가_직전대비1;
        public float 매수호가1;
        public int 매수호가_수량1;
        public float 매수호가_직전대비1;
        public float 매도호가2;
        public int 매도호가_수량2;
        public float 매도호가_직전대비2;
        public float 매수호가2;
        public int 매수호가_수량2;
        public float 매수호가_직전대비2;
        public float 매도호가3;
        public int 매도호가_수량3;
        public float 매도호가_직전대비3;
        public float 매수호가3;
        public int 매수호가_수량3;
        public float 매수호가_직전대비3;
        public float 매도호가4;
        public int 매도호가_수량4;
        public float 매도호가_직전대비4;
        public float 매수호가4;
        public int 매수호가_수량4;
        public float 매수호가_직전대비4;
        public float 매도호가5;
        public int 매도호가_수량5;
        public float 매도호가_직전대비5;
        public float 매수호가5;
        public int 매수호가_수량5;
        public float 매수호가_직전대비5;
        public float 매도호가6;
        public int 매도호가_수량6;
        public float 매도호가_직전대비6;
        public float 매수호가6;
        public int 매수호가_수량6;
        public float 매수호가_직전대비6;
        public float 매도호가7;
        public int 매도호가_수량7;
        public float 매도호가_직전대비7;
        public float 매수호가7;
        public int 매수호가_수량7;
        public float 매수호가_직전대비7;
        public float 매도호가8;
        public int 매도호가_수량8;
        public float 매도호가_직전대비8;
        public float 매수호가8;
        public int 매수호가_수량8;
        public float 매수호가_직전대비8;
        public float 매도호가9;
        public int 매도호가_수량9;
        public float 매도호가_직전대비9;
        public float 매수호가9;
        public int 매수호가_수량9;
        public float 매수호가_직전대비9;
        public float 매도호가10;
        public int 매도호가_수량10;
        public float 매도호가_직전대비10;
        public float 매수호가10;
        public int 매수호가_수량10;
        public float 매수호가_직전대비10;
        public long 매도호가_총잔량;
        public long 매도호가_총잔량_직전대비;
        public long 매수호가_총잔량;
        public long 매수호가_총잔량_직전대비;
        public float 예상체결가_;
        public long 예상체결_수량;
        public long 순매수잔량_총매수잔량_총매도잔량_;
        public float 매수비율;
        public long 순매도잔량_총매도잔량_총매수잔량_;
        public float 매도비율;
        public float 예상체결가_전일종가_대비;
        public float 예상체결가_전일종가_대비_등락율;
        public float 예상체결가_전일종가_대비기호;
        public float 예상체결가;
        public long 예상체결량;
        public string 예상체결가_전일대비_기호;
        public float 예상체결가_전일대비;
        public float 예상체결가_전일대비_등락율;
        public int LP매도호가_수량1;
        public int LP매수호가_수량1;
        public int LP매도호가_수량2;
        public int LP매수호가_수량2;
        public int LP매도호가_수량3;
        public int LP매수호가_수량3;
        public int LP매도호가_수량4;
        public int LP매수호가_수량4;
        public int LP매도호가_수량5;
        public int LP매수호가_수량5;
        public int LP매도호가_수량6;
        public int LP매수호가_수량6;
        public int LP매도호가_수량7;
        public int LP매수호가_수량7;
        public int LP매도호가_수량8;
        public int LP매수호가_수량8;
        public int LP매도호가_수량9;
        public int LP매수호가_수량9;
        public int LP매도호가_수량10;
        public int LP매수호가_수량10;
        public long 누적거래량;
        public float 전일거래량대비예상체결률;
        public string 장운영구분;
        public string 투자자별_ticker;
    }
    public class Tr선물시세 : TrStructure
    {

        public string 체결시간__HHMMSS_;
        public float 현재가__실시간종가;
        public float 전일_대비;
        public float 등락율;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public long 거래량;
        public long 누적거래량;
        public long 누적거래대금;
        public float 시가;
        public float 고가;
        public float 저가;
        public long 미결제_약정;
        public float 이론가;
        public float 이론베이시스;
        public string 매수_거래원_색깔3;
        public float 괴리율;
        public long 미결제_약정_전일대비;
        public float 괴리도;
        public string 전일대비_기호;
        public float KOSPI200;
        public int 전일거래량_대비_계약;
        public long 시초_미결제_약정수량;
        public long 초고_미결제_약정수량;
        public long 최저_미결제_약정수량;
        public float 전일거래량_대비_비율_;
        public long 미결제_증감;
    }
    public class Tr선물호가잔량 : TrStructure
    {

        public string 호가시간;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public float 매도호가1;
        public int 매도호가_수량1;
        public float 매도호가_직전대비1;
        public float 매도호가_건수1;
        public float 매수호가1;
        public int 매수호가_수량1;
        public float 매수호가_직전대비1;
        public float 매수호가_건수1;
        public float 매도호가2;
        public int 매도호가_수량2;
        public float 매도호가_직전대비2;
        public float 매도호가_건수2;
        public float 매수호가2;
        public int 매수호가_수량2;
        public float 매수호가_직전대비2;
        public float 매수호가_건수2;
        public float 매도호가3;
        public int 매도호가_수량3;
        public float 매도호가_직전대비3;
        public float 매도호가_건수3;
        public float 매수호가3;
        public int 매수호가_수량3;
        public float 매수호가_직전대비3;
        public float 매수호가_건수3;
        public float 매도호가4;
        public int 매도호가_수량4;
        public float 매도호가_직전대비4;
        public float 매도호가_건수4;
        public float 매수호가4;
        public int 매수호가_수량4;
        public float 매수호가_직전대비4;
        public float 매수호가_건수4;
        public float 매도호가5;
        public int 매도호가_수량5;
        public float 매도호가_직전대비5;
        public float 매도호가_건수5;
        public float 매수호가5;
        public int 매수호가_수량5;
        public float 매수호가_직전대비5;
        public float 매수호가_건수5;
        public long 매도호가_총잔량;
        public long 매도호가_총잔량_직전대비;
        public long 매도호가_총_건수;
        public long 매수호가_총잔량;
        public long 매수호가_총잔량_직전대비;
        public long 매수호가_총_건수;
        public long 호가_순잔량;
        public long 순매수잔량_총매수잔량_총매도잔량_;
        public long 누적거래량;
        public float 예상체결가_;
        public float 예상체결가_전일종가_대비기호;
        public float 예상체결가_전일종가_대비;
        public float 예상체결가_전일종가_대비_등락율;
        public float 예상체결가;
        public string 예상체결가_전일대비_기호;
        public float 예상체결가_전일대비;
        public float 예상체결가_전일대비_등락율;
    }
    public class Tr선물이론가 : TrStructure
    {

        public long 미결제_약정;
        public float 이론가;
        public float 이론베이시스;
        public string 매수_거래원_색깔3;
        public float 괴리율;
        public long 미결제_약정_전일대비;
        public float 괴리도;
        public long 시초_미결제_약정수량;
        public long 초고_미결제_약정수량;
        public long 최저_미결제_약정수량;
    }
    public class Tr옵션시세 : TrStructure
    {

        public string 체결시간__HHMMSS_;
        public float 현재가__실시간종가;
        public float 전일_대비;
        public float 등락율;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public long 거래량;
        public long 누적거래량;
        public long 누적거래대금;
        public float 시가;
        public float 고가;
        public float 저가;
        public long 미결제_약정;
        public float 이론가;
        public float 괴리율;
        public float 델타;
        public float 감마;
        public float 세타;
        public float 베가;
        public float 로;
        public long 미결제_약정_전일대비;
        public string 전일대비_기호;
        public int 전일거래량_대비_계약;
        public long 호가_순잔량;
        public float 내재가치;
        public float KOSPI200;
        public long 시초_미결제_약정수량;
        public long 초고_미결제_약정수량;
        public long 최저_미결제_약정수량;
        public float 선물_최근_월물지수;
        public long 미결제_증감;
        public float 시간가치;
        public float 내재변동성_I_V__;
        public float 전일거래량_대비_비율_;
        public float 기준가대비_시고등락율;
        public float 기준가대비_고가등락율;
        public float 기준가대비_저가등락율;
    }
    public class Tr옵션호가잔량 : TrStructure
    {

        public string 호가시간;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public float 매도호가1;
        public int 매도호가_수량1;
        public float 매도호가_직전대비1;
        public float 매도호가_건수1;
        public float 매수호가1;
        public int 매수호가_수량1;
        public float 매수호가_직전대비1;
        public float 매수호가_건수1;
        public float 매도호가2;
        public int 매도호가_수량2;
        public float 매도호가_직전대비2;
        public float 매도호가_건수2;
        public float 매수호가2;
        public int 매수호가_수량2;
        public float 매수호가_직전대비2;
        public float 매수호가_건수2;
        public float 매도호가3;
        public int 매도호가_수량3;
        public float 매도호가_직전대비3;
        public float 매도호가_건수3;
        public float 매수호가3;
        public int 매수호가_수량3;
        public float 매수호가_직전대비3;
        public float 매수호가_건수3;
        public float 매도호가4;
        public int 매도호가_수량4;
        public float 매도호가_직전대비4;
        public float 매도호가_건수4;
        public float 매수호가4;
        public int 매수호가_수량4;
        public float 매수호가_직전대비4;
        public float 매수호가_건수4;
        public float 매도호가5;
        public int 매도호가_수량5;
        public float 매도호가_직전대비5;
        public float 매도호가_건수5;
        public float 매수호가5;
        public int 매수호가_수량5;
        public float 매수호가_직전대비5;
        public float 매수호가_건수5;
        public long 매도호가_총잔량;
        public long 매도호가_총잔량_직전대비;
        public long 매도호가_총_건수;
        public long 매수호가_총잔량;
        public long 매수호가_총잔량_직전대비;
        public long 매수호가_총_건수;
        public long 호가_순잔량;
        public long 순매수잔량_총매수잔량_총매도잔량_;
        public long 누적거래량;
        public float 예상체결가_;
        public float 예상체결가_전일종가_대비기호;
        public float 예상체결가_전일종가_대비;
        public float 예상체결가_전일종가_대비_등락율;
        public float 예상체결가;
        public string 예상체결가_전일대비_기호;
        public float 예상체결가_전일대비;
        public float 예상체결가_전일대비_등락율;
    }
    public class Tr옵션이론가 : TrStructure
    {

        public long 미결제_약정;
        public float 이론가;
        public float 괴리율;
        public float 델타;
        public float 감마;
        public float 세타;
        public float 베가;
        public float 로;
        public long 미결제_약정_전일대비;
        public long 시초_미결제_약정수량;
        public long 초고_미결제_약정수량;
        public long 최저_미결제_약정수량;
        public float 내재가치;
        public float 시간가치;
        public float 내재변동성_I_V__;
    }
    public class Tr주식옵션시세 : TrStructure
    {

        public string 체결시간__HHMMSS_;
        public float 현재가__실시간종가;
        public float 전일_대비;
        public float 등락율;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public long 거래량;
        public long 누적거래량;
        public long 누적거래대금;
        public float 시가;
        public float 고가;
        public float 저가;
        public long 미결제_약정;
        public float 이론가;
        public float 괴리율;
        public float 델타;
        public float 감마;
        public float 세타;
        public float 베가;
        public float 로;
        public long 미결제_약정_전일대비;
        public string 전일대비_기호;
        public int 전일거래량_대비_계약;
    }
    public class Tr주식옵션호가잔량 : TrStructure
    {

        public string 호가시간;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public float 매도호가1;
        public float 매도호가2;
        public float 매도호가3;
        public float 매도호가4;
        public float 매도호가5;
        public int 매도호가_수량1;
        public int 매도호가_수량2;
        public int 매도호가_수량3;
        public int 매도호가_수량4;
        public int 매도호가_수량5;
        public float 매도호가_건수1;
        public float 매도호가_건수2;
        public float 매도호가_건수3;
        public float 매도호가_건수4;
        public float 매도호가_건수5;
        public float 매수호가1;
        public float 매수호가2;
        public float 매수호가3;
        public float 매수호가4;
        public float 매수호가5;
        public int 매수호가_수량1;
        public int 매수호가_수량2;
        public int 매수호가_수량3;
        public int 매수호가_수량4;
        public int 매수호가_수량5;
        public float 매수호가_건수1;
        public float 매수호가_건수2;
        public float 매수호가_건수3;
        public float 매수호가_건수4;
        public float 매수호가_건수5;
        public long 매도호가_총잔량;
        public long 매도호가_총_건수;
        public long 매수호가_총잔량;
        public long 매수호가_총_건수;
        public float 예상체결가_;
        public float 예상체결가_전일종가_대비기호;
        public float 예상체결가_전일종가_대비;
        public float 예상체결가_전일종가_대비_등락율;
        public float 예상체결가;
        public string 예상체결가_전일대비_기호;
        public float 예상체결가_전일대비;
        public float 예상체결가_전일대비_등락율;
    }
    public class Tr주식옵션이론가 : TrStructure
    {

        public long 미결제_약정;
        public float 이론가;
        public float 괴리율;
        public float 델타;
        public float 감마;
        public float 세타;
        public float 베가;
        public float 로;
        public long 미결제_약정_전일대비;
    }
    public class Tr주식옵션우선호가 : TrStructure
    {

        public float 현재가__실시간종가;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
    }
    public class Tr업종지수 : TrStructure
    {

        public string 체결시간__HHMMSS_;
        public float 현재가__실시간종가;
        public float 전일_대비;
        public float 등락율;
        public long 거래량;
        public long 누적거래량;
        public long 누적거래대금;
        public float 시가;
        public float 고가;
        public float 저가;
        public string 전일대비_기호;
        public int 전일거래량_대비_계약;
    }
    public class Tr업종등락 : TrStructure
    {

        public string 체결시간__HHMMSS_;
        public int 상승종목수;
        public int 상한종목수;
        public int 보합종목수;
        public int 하락종목수;
        public int 하한종목수;
        public long 누적거래량;
        public long 누적거래대금;
        public float 현재가__실시간종가;
        public float 전일_대비;
        public float 등락율;
        public int 거래형성_종목수;
        public float 거래형성_비율;
        public string 전일대비_기호;
    }
    public class Tr장시작시간 : TrStructure
    {

        public string 장운영구분;
        public string 체결시간__HHMMSS_;
        public string 장시작_예상잔여시간;
    }
    public class Tr투자자ticker : TrStructure
    {
        public string 투자자별_ticker;
    }
    public class Tr주문체결 : TrStructure
    {

        public string 계좌번호;
        public string 주문번호;
        public string 관리자사번;
        public string 종목코드__업종코드;
        public string 주문업무분류;
        public string 주문상태_접수__확인__체결_;
        public string 종목명;
        public int 주문수량;
        public float 주문가격;
        public int 미체결수량;
        public long 체결누계금액;
        public string 원주문번호;
        public string 주문구분;
        public string 매매구분;
        public string 매도수구분__1_매도_2_매수_;
        public string 주문_체결시간_HHMMSSMS_;
        public string 체결번호;
        public float 체결가;
        public int 체결량;
        public float 현재가__실시간종가;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public float 단위체결가;
        public int 단위체결량;
        public long 당일매매_수수료;
        public long 당일매매세금;
    }
    public class Tr잔고 : TrStructure
    {

        public string 계좌번호;
        public string 종목코드__업종코드;
        public string 종목명;
        public float 현재가__실시간종가;
        public int 보유수량;
        public float 매입단가;
        public long 총매입가;
        public int 주문가능수량;
        public long 당일순매수량;
        public string 매도_매수_구분;
        public long 당일_총_매도_손익;
        public long 예수금;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public float 기준가;
        public float 손익율;
        public string 주식옵션거래단위;
    }
    public class Tr신용잔고 : TrStructure
    {

        public string 계좌번호;
        public string 종목코드__업종코드;
        public string 신용구분;
        public string 대출일;
        public string 종목명;
        public float 현재가__실시간종가;
        public int 보유수량;
        public float 매입단가;
        public long 총매입가;
        public int 주문가능수량;
        public long 당일순매수량;
        public string 매도_매수_구분;
        public long 당일_총_매도_손익;
        public long 예수금;
        public float _최우선_매도호가;
        public float _최우선_매수호가;
        public float 기준가;
        public float 손익율;
        public long 신용금액;
        public float 신용이자;
        public string 만기일;
        public long 당일실현손익_유가_;
        public float 당일실현손익률_유가_;
        public long 당일실현손익_신용_;
        public float 당일실현손익률_신용_;
        public long 담보대출수량;
    }
    public class Tr주식시간외호가 : TrStructure
    {

        public string 호가시간;
        public long 시간외_매도호가_총잔량;
        public long 시간외_매도호가_총잔량_직전대비;
        public long 시간외_매수호가_총잔량;
        public long 시간외_매수호가_총잔량_직전대비;
    }
    public class Tr주식당일거래원 : TrStructure
    {

        public string 매도_거래원1;
        public long 매도_거래원_수량1;
        public long 매도_거래원별_증감1;
        public string 매도_거래원_코드1;
        public string 매도_거래원_색깔1;
        public string 매수_거래원1;
        public long 매수_거래원_수량1;
        public long 매수_거래원별_증감1;
        public string 매수_거래원_코드1;
        public string 매수_거래원_색깔1;
        public string 매도_거래원2;
        public long 매도_거래원_수량2;
        public long 매도_거래원별_증감2;
        public string 매도_거래원_코드2;
        public string 매도_거래원_색깔2;
        public string 매수_거래원2;
        public long 매수_거래원_수량2;
        public long 매수_거래원별_증감2;
        public string 매수_거래원_코드2;
        public string 매수_거래원_색깔2;
        public string 매도_거래원3;
        public long 매도_거래원_수량3;
        public long 매도_거래원별_증감3;
        public string 매도_거래원_코드3;
        public string 매도_거래원_색깔3;
        public string 매수_거래원;
        public long 매수_거래원_수량3;
        public long 매수_거래원별_증감3;
        public string 매수_거래원_코드3;
        public string 매수_거래원_색깔3;
        public string 매도_거래원4;
        public long 매도_거래원_수량4;
        public long 매도_거래원별_증감4;
        public string 매도_거래원_코드4;
        public string 매도_거래원_색깔4;
        public string 매수_거래원4;
        public long 매수_거래원_수량4;
        public long 매수_거래원별_증감4;
        public string 매수_거래원_코드4;
        public string 매수_거래원_색깔4;
        public string 매도_거래원5;
        public long 매도_거래원_수량5;
        public long 매도_거래원별_증감5;
        public string 매도_거래원_코드5;
        public string 매도_거래원_색깔5;
        public string 매수_거래원5;
        public long 매수_거래원_수량5;
        public long 매수_거래원별_증감5;
        public string 매수_거래원_코드5;
        public string 매수_거래원_색깔5;
        public long 외국계_매도추정합;
        public float 외국계_매도추정합_변동;
        public long 외국계_매수추정합;
        public float 외국계_매수추정합_변동;
        public long 외국계_순매수추정합;
        public float 외국계_순매수_변동;
        public string 거래소구분;
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


}
