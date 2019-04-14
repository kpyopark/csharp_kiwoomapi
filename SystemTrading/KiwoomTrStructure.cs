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


    public class TrStockPrice : TrStructure
    {

        public float currprice_currclose;
        public float prevday_contrast;
        public float hlratio;
        public float _prefer_bid;
        public float _prefer_ask;
        public float acctamt;
        public long acctvalue;
        public float currprice;
        public float high;
        public float low;
        public string contpervday_symbol;
        public float prevtxamt_contrast_contract;
        public float tvalue_inde;
        public float prevtxamt_contrast_ratio;
        public float toratio;
        public long tfee;
        public long currmc;
    }
    public class TrStockTransaction : TrStructure
    {

        public string ttime_hhmmss;
        public float currprice_currclose;
        public float prevday_contrast;
        public float hlratio;
        public float _prefer_bid;
        public float _prefer_ask;
        public long tamount;
        public float acctamt;
        public long acctvalue;
        public float currprice;
        public float high;
        public float low;
        public string contpervday_symbol;
        public float prevtxamt_contrast_contract;
        public float tvalue_inde;
        public float prevtxamt_contrast_ratio;
        public float toratio;
        public long tfee;
        public long tintensity;
        public long currmc;
        public string mtype;
        public float k_o_approx;
    }
    public class TrStockFirstCallPrice : TrStructure
    {

        public float _prefer_bid;
        public float _prefer_ask;
    }
    public class TrStockSpread : TrStructure
    {

        public string quotetime;
        public float bid1;
        public float bid_amt1;
        public float bid_contrastprevtick1;
        public float ask1;
        public float ask_amt1;
        public float ask_contrastprevtick1;
        public float bid2;
        public float bid_amt2;
        public float bid_contrastprevtick2;
        public float ask2;
        public float ask_amt2;
        public float ask_contrastprevtick2;
        public float bid3;
        public float bid_amt3;
        public float bid_contrastprevtick3;
        public float ask3;
        public float ask_amt3;
        public float ask_contrastprevtick3;
        public float bid4;
        public float bid_amt4;
        public float bid_contrastprevtick4;
        public float ask4;
        public float ask_amt4;
        public float ask_contrastprevtick4;
        public float bid5;
        public float bid_amt5;
        public float bid_contrastprevtick5;
        public float ask5;
        public float ask_amt5;
        public float ask_contrastprevtick5;
        public float bid6;
        public float bid_amt6;
        public float bid_contrastprevtick6;
        public float ask6;
        public float ask_amt6;
        public float ask_contrastprevtick6;
        public float bid7;
        public float bid_amt7;
        public float bid_contrastprevtick7;
        public float ask7;
        public float ask_amt7;
        public float ask_contrastprevtick7;
        public float bid8;
        public float bid_amt8;
        public float bid_contrastprevtick8;
        public float ask8;
        public float ask_amt8;
        public float ask_contrastprevtick8;
        public float bid9;
        public float bid_amt9;
        public float bid_contrastprevtick9;
        public float ask9;
        public float ask_amt9;
        public float ask_contrastprevtick9;
        public float bid10;
        public float bid_amt10;
        public float bid_contrastprevtick10;
        public float ask10;
        public float ask_amt10;
        public float ask_contrastprevtick10;
        public long bid_tredis;
        public long bid_tredis_contrastprevtick;
        public long ask_tredis;
        public long ask_tredis_contrastprevtick;
        public float estimcontractprice_;
        public float estimcontract_amt;
        public long netaskspread_taskresids_tbidresids;
        public float askratio;
        public long netbidspread_tbidresids_taskresids;
        public float bidratio;
        public float estimcontractprice_prevdayclose_contrast;
        public float estimcontractprice_prevdayclose_contrast_hlratio;
        public float estimcontractprice_prevdayclose_contrastsymbol;
        public float estimcontractprice;
        public float estimcontractamt;
        public string estimcontractprice_contpervday_symbol;
        public float estimcontractprice_contpervday;
        public float estimcontractprice_contpervday_hlratio;
        public float lpbid_amt1;
        public float lpask_amt1;
        public float lpbid_amt2;
        public float lpask_amt2;
        public float lpbid_amt3;
        public float lpask_amt3;
        public float lpbid_amt4;
        public float lpask_amt4;
        public float lpbid_amt5;
        public float lpask_amt5;
        public float lpbid_amt6;
        public float lpask_amt6;
        public float lpbid_amt7;
        public float lpask_amt7;
        public float lpbid_amt8;
        public float lpask_amt8;
        public float lpbid_amt9;
        public float lpask_amt9;
        public float lpbid_amt10;
        public float lpask_amt10;
        public float acctamt;
        public float estim_contractratio_prevtxamt;
        public string mmngttype;
        public string ptrader_ticker;
    }
    public class TrFuturePrice : TrStructure
    {

        public string ttime_hhmmss;
        public float currprice_currclose;
        public float prevday_contrast;
        public float hlratio;
        public float _prefer_bid;
        public float _prefer_ask;
        public long tamount;
        public float acctamt;
        public long acctvalue;
        public float currprice;
        public float high;
        public float low;
        public long opened_agr;
        public float theoprice;
        public float theobasis;
        public string ask_trader_color3;
        public float disjunctratio;
        public long opened_agr_contpervday;
        public float disjunction;
        public string contpervday_symbol;
        public float kospi200;
        public float prevtxamt_contrast_contract;
        public float open_opened_agramt;
        public float max_opened_agramt;
        public float min_opened_agramt;
        public float prevtxamt_contrast_ratio;
        public long opened_inde;
    }
    public class TrFutureQuoteSpread : TrStructure
    {

        public string quotetime;
        public float _prefer_bid;
        public float _prefer_ask;
        public float bid1;
        public float bid_amt1;
        public float bid_contrastprevtick1;
        public float bid_count1;
        public float ask1;
        public float ask_amt1;
        public float ask_contrastprevtick1;
        public float ask_count1;
        public float bid2;
        public float bid_amt2;
        public float bid_contrastprevtick2;
        public float bid_count2;
        public float ask2;
        public float ask_amt2;
        public float ask_contrastprevtick2;
        public float ask_count2;
        public float bid3;
        public float bid_amt3;
        public float bid_contrastprevtick3;
        public float bid_count3;
        public float ask3;
        public float ask_amt3;
        public float ask_contrastprevtick3;
        public float ask_count3;
        public float bid4;
        public float bid_amt4;
        public float bid_contrastprevtick4;
        public float bid_count4;
        public float ask4;
        public float ask_amt4;
        public float ask_contrastprevtick4;
        public float ask_count4;
        public float bid5;
        public float bid_amt5;
        public float bid_contrastprevtick5;
        public float bid_count5;
        public float ask5;
        public float ask_amt5;
        public float ask_contrastprevtick5;
        public float ask_count5;
        public long bid_tredis;
        public long bid_tredis_contrastprevtick;
        public long bid_tcount;
        public long ask_tredis;
        public long ask_tredis_contrastprevtick;
        public long ask_tcount;
        public long quoteprice_netspread;
        public long netaskspread_taskresids_tbidresids;
        public float acctamt;
        public float estimcontractprice_;
        public float estimcontractprice_prevdayclose_contrastsymbol;
        public float estimcontractprice_prevdayclose_contrast;
        public float estimcontractprice_prevdayclose_contrast_hlratio;
        public float estimcontractprice;
        public string estimcontractprice_contpervday_symbol;
        public float estimcontractprice_contpervday;
        public float estimcontractprice_contpervday_hlratio;
    }
    public class TrFutureTheoPrice : TrStructure
    {

        public long opened_agr;
        public float theoprice;
        public float theobasis;
        public string ask_trader_color3;
        public float disjunctratio;
        public long opened_agr_contpervday;
        public float disjunction;
        public float open_opened_agramt;
        public float max_opened_agramt;
        public float min_opened_agramt;
    }
    public class TrOptionPrice : TrStructure
    {

        public string ttime_hhmmss;
        public float currprice_currclose;
        public float prevday_contrast;
        public float hlratio;
        public float _prefer_bid;
        public float _prefer_ask;
        public long tamount;
        public float acctamt;
        public long acctvalue;
        public float currprice;
        public float high;
        public float low;
        public long opened_agr;
        public float theoprice;
        public float disjunctratio;
        public float delta;
        public float gamma;
        public float theta;
        public float vega;
        public float rho;
        public long opened_agr_contpervday;
        public string contpervday_symbol;
        public float prevtxamt_contrast_contract;
        public long quoteprice_netspread;
        public float iv;
        public float kospi200;
        public float open_opened_agramt;
        public float max_opened_agramt;
        public float min_opened_agramt;
        public float future_curr_mnthitemindex;
        public long opened_inde;
        public float timevalue;
        public float ivv_iv;
        public float prevtxamt_contrast_ratio;
        public float spricediff_ohratio;
        public float spricediff_hcratio;
        public float spricediff_lcratio;
    }
    public class TrOptionQuoteSpread : TrStructure
    {

        public string quotetime;
        public float _prefer_bid;
        public float _prefer_ask;
        public float bid1;
        public float bid_amt1;
        public float bid_contrastprevtick1;
        public float bid_count1;
        public float ask1;
        public float ask_amt1;
        public float ask_contrastprevtick1;
        public float ask_count1;
        public float bid2;
        public float bid_amt2;
        public float bid_contrastprevtick2;
        public float bid_count2;
        public float ask2;
        public float ask_amt2;
        public float ask_contrastprevtick2;
        public float ask_count2;
        public float bid3;
        public float bid_amt3;
        public float bid_contrastprevtick3;
        public float bid_count3;
        public float ask3;
        public float ask_amt3;
        public float ask_contrastprevtick3;
        public float ask_count3;
        public float bid4;
        public float bid_amt4;
        public float bid_contrastprevtick4;
        public float bid_count4;
        public float ask4;
        public float ask_amt4;
        public float ask_contrastprevtick4;
        public float ask_count4;
        public float bid5;
        public float bid_amt5;
        public float bid_contrastprevtick5;
        public float bid_count5;
        public float ask5;
        public float ask_amt5;
        public float ask_contrastprevtick5;
        public float ask_count5;
        public long bid_tredis;
        public long bid_tredis_contrastprevtick;
        public long bid_tcount;
        public long ask_tredis;
        public long ask_tredis_contrastprevtick;
        public long ask_tcount;
        public long quoteprice_netspread;
        public long netaskspread_taskresids_tbidresids;
        public float acctamt;
        public float estimcontractprice_;
        public float estimcontractprice_prevdayclose_contrastsymbol;
        public float estimcontractprice_prevdayclose_contrast;
        public float estimcontractprice_prevdayclose_contrast_hlratio;
        public float estimcontractprice;
        public string estimcontractprice_contpervday_symbol;
        public float estimcontractprice_contpervday;
        public float estimcontractprice_contpervday_hlratio;
    }
    public class TrOptionTheoPrice : TrStructure
    {

        public long opened_agr;
        public float theoprice;
        public float disjunctratio;
        public float delta;
        public float gamma;
        public float theta;
        public float vega;
        public float rho;
        public long opened_agr_contpervday;
        public float open_opened_agramt;
        public float max_opened_agramt;
        public float min_opened_agramt;
        public float iv;
        public float timevalue;
        public float ivv_iv;
    }
    public class TrStockOptionPrice : TrStructure
    {

        public string ttime_hhmmss;
        public float currprice_currclose;
        public float prevday_contrast;
        public float hlratio;
        public float _prefer_bid;
        public float _prefer_ask;
        public long tamount;
        public float acctamt;
        public long acctvalue;
        public float currprice;
        public float high;
        public float low;
        public long opened_agr;
        public float theoprice;
        public float disjunctratio;
        public float delta;
        public float gamma;
        public float theta;
        public float vega;
        public float rho;
        public long opened_agr_contpervday;
        public string contpervday_symbol;
        public float prevtxamt_contrast_contract;
    }
    public class TrStockOptionQuoteSpread : TrStructure
    {

        public string quotetime;
        public float _prefer_bid;
        public float _prefer_ask;
        public float bid1;
        public float bid2;
        public float bid3;
        public float bid4;
        public float bid5;
        public float bid_amt1;
        public float bid_amt2;
        public float bid_amt3;
        public float bid_amt4;
        public float bid_amt5;
        public float bid_count1;
        public float bid_count2;
        public float bid_count3;
        public float bid_count4;
        public float bid_count5;
        public float ask1;
        public float ask2;
        public float ask3;
        public float ask4;
        public float ask5;
        public float ask_amt1;
        public float ask_amt2;
        public float ask_amt3;
        public float ask_amt4;
        public float ask_amt5;
        public float ask_count1;
        public float ask_count2;
        public float ask_count3;
        public float ask_count4;
        public float ask_count5;
        public long bid_tredis;
        public long bid_tcount;
        public long ask_tredis;
        public long ask_tcount;
        public float estimcontractprice_;
        public float estimcontractprice_prevdayclose_contrastsymbol;
        public float estimcontractprice_prevdayclose_contrast;
        public float estimcontractprice_prevdayclose_contrast_hlratio;
        public float estimcontractprice;
        public string estimcontractprice_contpervday_symbol;
        public float estimcontractprice_contpervday;
        public float estimcontractprice_contpervday_hlratio;
    }
    public class TrStockOptionTheoPrice : TrStructure
    {

        public long opened_agr;
        public float theoprice;
        public float disjunctratio;
        public float delta;
        public float gamma;
        public float theta;
        public float vega;
        public float rho;
        public long opened_agr_contpervday;
    }
    public class TrStockOptionPreferPrice : TrStructure
    {

        public float currprice_currclose;
        public float _prefer_bid;
        public float _prefer_ask;
    }
    public class TrIndustrialIndex : TrStructure
    {

        public string ttime_hhmmss;
        public float currprice_currclose;
        public float prevday_contrast;
        public float hlratio;
        public long tamount;
        public float acctamt;
        public long acctvalue;
        public float currprice;
        public float high;
        public float low;
        public string contpervday_symbol;
        public float prevtxamt_contrast_contract;
    }
    public class TrIndustrialHighLowCount : TrStructure
    {

        public string ttime_hhmmss;
        public int bullcnt;
        public int highcnt;
        public int surgecnt;
        public int bearcnt;
        public int lowcnt;
        public float acctamt;
        public long acctvalue;
        public float currprice_currclose;
        public float prevday_contrast;
        public float hlratio;
        public int tx_stockcnt;
        public float tx_ratio;
        public string contpervday_symbol;
    }
    public class TrMarketOpenTime : TrStructure
    {

        public string mmngttype;
        public string ttime_hhmmss;
        public string mopen_estimresidtime;
    }
    public class TrTraderTicker : TrStructure
    {

        public string ptrader_ticker;
    }
    public class TrOrderContract : TrStructure
    {

        public string account;
        public string orderid;
        public string mngrid;
        public string stockid_indcode;
        public string orderoptype;
        public string orderstat;
        public string stockname;
        public float orderamt;
        public float orderprice;
        public float openedamt;
        public long accutvalue;
        public string orgordid;
        public string ordertype;
        public string ttype;
        public string bidasktype;
        public string order_ttime_hhmmssms;
        public string tidt;
        public float tprice;
        public float tamt;
        public float currprice_currclose;
        public float _prefer_bid;
        public float _prefer_ask;
        public float unitprice;
        public int unitamount;
        public long todaytx_comms;
        public long todaytxtax;
    }
    public class TrTotalAccount : TrStructure
    {

        public string account;
        public string stockid_indcode;
        public string stockname;
        public float currprice_currclose;
        public int possquant;
        public float askprice;
        public long taskprice;
        public float orderableamt;
        public float todaytxamt;
        public string bid_ask_type;
        public long today_total_bid_pl;
        public long deposit;
        public float _prefer_bid;
        public float _prefer_ask;
        public float sprice;
        public float plratio;
        public string orderunit;
    }
    public class TrCreditAccount : TrStructure
    {

        public string account;
        public string stockid_indcode;
        public string credittype;
        public string debtdate;
        public string stockname;
        public float currprice_currclose;
        public int possquant;
        public float askprice;
        public long taskprice;
        public float orderableamt;
        public float todaytxamt;
        public string bid_ask_type;
        public long today_total_bid_pl;
        public long deposit;
        public float _prefer_bid;
        public float _prefer_ask;
        public float sprice;
        public float plratio;
        public float creditamt;
        public float creditint;
        public string duedate;
        public long todayrealizedpl_oilprice;
        public float todayrealizedplratio_oilprice;
        public long todayrealizedpl_credit;
        public float todayrealizedplratio_credit;
        public float mortloanamt;
    }
    public class TrAfterHour : TrStructure
    {

        public string quotetime;
        public long aftertime_bid_tredis;
        public long aftertime_bid_tredis_contrastprevtick;
        public long aftertime_ask_tredis;
        public long aftertime_ask_tredis_contrastprevtick;
    }
    public class TrStockTraders : TrStructure
    {

        public string bid_trader1;
        public float bid_trader_amt1;
        public long bid_traders_inde1;
        public string bid_trader_code1;
        public string bid_trader_color1;
        public string ask_trader1;
        public float ask_trader_amt1;
        public long ask_traders_inde1;
        public string ask_trader_code1;
        public string ask_trader_color1;
        public string bid_trader2;
        public float bid_trader_amt2;
        public long bid_traders_inde2;
        public string bid_trader_code2;
        public string bid_trader_color2;
        public string ask_trader2;
        public float ask_trader_amt2;
        public long ask_traders_inde2;
        public string ask_trader_code2;
        public string ask_trader_color2;
        public string bid_trader3;
        public float bid_trader_amt3;
        public long bid_traders_inde3;
        public string bid_trader_code3;
        public string bid_trader_color3;
        public string ask_trader;
        public float ask_trader_amt3;
        public long ask_traders_inde3;
        public string ask_trader_code3;
        public string ask_trader_color3;
        public string bid_trader4;
        public float bid_trader_amt4;
        public long bid_traders_inde4;
        public string bid_trader_code4;
        public string bid_trader_color4;
        public string ask_trader4;
        public float ask_trader_amt4;
        public long ask_traders_inde4;
        public string ask_trader_code4;
        public string ask_trader_color4;
        public string bid_trader5;
        public float bid_trader_amt5;
        public long bid_traders_inde5;
        public string bid_trader_code5;
        public string bid_trader_color5;
        public string ask_trader5;
        public float ask_trader_amt5;
        public long ask_traders_inde5;
        public string ask_trader_code5;
        public string ask_trader_color5;
        public float foreign_estimbidamt;
        public float foreign_estimbidamt_vlt;
        public float foreign_estimaskamt;
        public float foreign_estimaskamt_vlt;
        public long foreign_estimasksum;
        public float foreign_netask_vlt;
        public string mtype;
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
