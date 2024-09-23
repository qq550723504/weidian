// Decompiled with JetBrains decompiler
// Type: Auxiliary.Comm.Model
// Assembly: 微店新版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41556A13-EB55-4869-9F07-F86BE27F7881
// Assembly location: C:\Users\xuwei\Documents\WeChat Files\xuwexia\FileStorage\File\2024-08\微店程序\微店程序\微店新版6.18.exe

using System.Collections.Generic;


namespace Auxiliary.Comm
{
  public class Model
  {
    public class plinfo
    {
      public string id { get; set; }

      public string pcid { get; set; }

      public string phone { get; set; }

      public string cookie { get; set; }

      public string shopid { get; set; }

      public string orderid { get; set; }

      public string itemid { get; set; }

      public string accstatus { get; set; }

      public string status { get; set; }
    }

    public class Status
    {
      public string code { get; set; }

      public string message { get; set; }

      public string description { get; set; }
    }

    public class Items
    {
      public string buyNum { get; set; }

      public string circleItemBuyTimes { get; set; }

      public string evaluationNum { get; set; }

      public string experienceNum { get; set; }

      public bool isFx { get; set; }

      public string isPointPrice { get; set; }

      public bool isPreSale { get; set; }

      public string itemId { get; set; }

      public string itemMainPic { get; set; }

      public string itemName { get; set; }

      public string itemPrice { get; set; }

      public string itemStatus { get; set; }

      public string itemType { get; set; }

      public bool mcSafeSpu { get; set; }

      public bool newItem { get; set; }

      public bool pointItem { get; set; }

      public bool present { get; set; }

      public string recommendReason { get; set; }

      public string regularCustomerNum { get; set; }

      public string spoor { get; set; }

      public bool supportInstallment { get; set; }

      public bool taobaoShopIcon { get; set; }

      public string time { get; set; }

      public bool timingSold { get; set; }

      public string timingSoldDuration { get; set; }

      public bool tmallShopIcon { get; set; }

      public bool videoItem { get; set; }
    }

    public class ShopInfo
    {
      public string buyNum { get; set; }

      public string collectCount { get; set; }

      public string cpsIcon { get; set; }

      public bool fx { get; set; }

      public string groupNum { get; set; }

      public bool hideItemSale { get; set; }

      public string isBaking { get; set; }

      public string isCraftsman { get; set; }

      public string isFarmer { get; set; }

      public string isGlobalShop { get; set; }

      public string isKitchen { get; set; }

      public string payedTotal { get; set; }

      public string regularCustomerNum { get; set; }

      public string repurRatePercent { get; set; }

      public string sayingNum { get; set; }

      public string sellerId { get; set; }

      public string shopBgImgUrl { get; set; }

      public string shopCreditNum { get; set; }

      public string shopCreditType { get; set; }

      public string shopDesc { get; set; }

      public string shopGrade { get; set; }

      public string shopId { get; set; }

      public string shopLogo { get; set; }

      public string shopName { get; set; }

      public string shopNote { get; set; }

      public string status { get; set; }

      public bool taobaoShopIcon { get; set; }

      public bool tmallShopIcon { get; set; }
    }

    public class Result
    {
      public string itemDesc { get; set; }

      public List<Model.Items> items { get; set; }

      public Model.ShopInfo shopInfo { get; set; }
    }

    public class MD
    {
      public Model.Status status { get; set; }

      public Model.Result result { get; set; }
    }

    public class 流星登录实体
    {
      public string msg { get; set; }

      public int code { get; set; }

      public double money { get; set; }

      public int maxCode { get; set; }

      public string token { get; set; }
    }

    public class 流星接码实体
    {
      public string msg { get; set; }

      public int code { get; set; }

      public string phone { get; set; }

      public string sms { get; set; }
    }

    public class creparam
    {
      public string param { get; set; }

      public string content { get; set; }

      public string udc { get; set; }

      public string wdtoken { get; set; }
    }

    public class Detail
    {
      public string randstr { get; set; }

      public string ticket { get; set; }
    }

    public class 腾讯滑块
    {
      public string errorCode { get; set; }

      public string randstr { get; set; }

      public string ticket { get; set; }

      public string errMessage { get; set; }

      public string sess { get; set; }

      public string type { get; set; }

      public string status { get; set; }

      public string expire { get; set; }

      public Model.Detail Detail { get; set; }
    }

    public class 微店verify_Result
    {
      public string session { get; set; }
    }

    public class 微店verify_Status
    {
      public int status_code { get; set; }

      public string status_reason { get; set; }
    }

    public class 微店verify
    {
      public Model.微店verify_Result result { get; set; }

      public Model.微店verify_Status status { get; set; }
    }

    public class 微店vcode_Status
    {
      public int code { get; set; }

      public string message { get; set; }

      public string description { get; set; }
    }

    public class 微店vcode
    {
      public Model.微店vcode_Status status { get; set; }

      public string result { get; set; }
    }

    public class 微店_PAYStatus
    {
      public int code { get; set; }

      public string message { get; set; }

      public string description { get; set; }
    }

    public class Biz_info
    {
      public string source_id { get; set; }
    }

    public class Buyer
    {
      public string buyer_id { get; set; }
    }

    public class Order_list
    {
      public string order_id { get; set; }

      public string order_s { get; set; }
    }

    public class 微店_PAYResult
    {
      public Model.Biz_info biz_info { get; set; }

      public Model.Buyer buyer { get; set; }

      public List<Model.Order_list> order_list { get; set; }

      public string pay_mode { get; set; }

      public string pvid { get; set; }

      public string url { get; set; }
    }

    public class 微店_PAY
    {
      public Model.微店_PAYStatus status { get; set; }

      public Model.微店_PAYResult result { get; set; }
    }

    public class 微店_perStatus
    {
      public int code { get; set; }

      public string message { get; set; }

      public string description { get; set; }
    }

    public class AliPay
    {
      public string notifyToken { get; set; }

      public string payUrl { get; set; }
    }

    public class PayResult
    {
    }

    public class hexiaorootStatus
    {
      public string code { get; set; }

      public string message { get; set; }

      public string description { get; set; }
    }

    public class hexiaorootExt
    {
    }

    public class hexiaorootButtons
    {
      public Model.hexiaorootExt ext { get; set; }

      public string text { get; set; }

      public string type { get; set; }

      public string url { get; set; }
    }

    public class hexiaorootFlagInfo
    {
      public string canBatchOperate { get; set; }

      public string canUseOnlineExpress { get; set; }

      public string ifCanBatchNoLogistics { get; set; }

      public string ifCanRepay { get; set; }

      public string ifCanShip { get; set; }

      public string ifCanVerify { get; set; }

      public string ifCityExpressOrder { get; set; }

      public string ifCrossBorderOrder { get; set; }

      public string ifGhSeller { get; set; }

      public string ifNeedBuyerRepay { get; set; }

      public string ifPickGroupOrder { get; set; }

      public string ifSelfDeliveryOrder { get; set; }

      public string ifWttOrder { get; set; }

      public string ifWttSelfOrder { get; set; }

      public string isPeriodOrder { get; set; }

      public string isTicketOrder { get; set; }

      public string isWeiOrder { get; set; }
    }

    public class hexiaorootItemList
    {
      public string canDeliver { get; set; }

      public string canVerify { get; set; }

      public List<string> contactInfo { get; set; }

      public string couldJump { get; set; }

      public string curShopItemId { get; set; }

      public string ifGift { get; set; }

      public string imgHead { get; set; }

      public string isChangePurchase { get; set; }

      public string isDelivered { get; set; }

      public string isPresent { get; set; }

      public string itemId { get; set; }

      public string itemName { get; set; }

      public string itemSkuId { get; set; }

      public string merchantCode { get; set; }

      public string point { get; set; }

      public string posWeightOrder { get; set; }

      public string price { get; set; }

      public string quantity { get; set; }

      public string refundStatus { get; set; }

      public string refundStatusStr { get; set; }

      public string skuMerchant { get; set; }

      public string status { get; set; }

      public string totalPrice { get; set; }

      public string verifyStatusStr { get; set; }
    }

    public class hexiaorootReceiver
    {
      public string buyerAddress { get; set; }

      public string buyerBindTelephone { get; set; }

      public string buyerId { get; set; }

      public string buyerName { get; set; }

      public string buyerNote { get; set; }

      public string buyerTelephone { get; set; }

      public string buyerWxNickName { get; set; }

      public string fSellerNotes { get; set; }

      public string ghSellerNote { get; set; }

      public string helpSellerNote { get; set; }

      public string idCardNo { get; set; }

      public string ifBuyerModifyAddr { get; set; }

      public string orderNum { get; set; }

      public string sellerNote { get; set; }
    }

    public class hexiaorootOrderList
    {
      public string addTime { get; set; }

      public List<Model.hexiaorootButtons> buttons { get; set; }

      public string canUpdateHelpSellerNotes { get; set; }

      public string canUpdateNote { get; set; }

      public string cityExpressStatus { get; set; }

      public List<string> contactInfo { get; set; }

      public string expressFee { get; set; }

      public string fCanUpdateNotes { get; set; }

      public Model.hexiaorootFlagInfo flagInfo { get; set; }

      public string fxLevel { get; set; }

      public string ghCanUpdateNotes { get; set; }

      public List<Model.hexiaorootItemList> itemList { get; set; }

      public string orderChannel { get; set; }

      public string orderChannelDesc { get; set; }

      public string orderId { get; set; }

      public string payId { get; set; }

      public string point { get; set; }

      public string quantity { get; set; }

      public Model.hexiaorootReceiver receiver { get; set; }

      public string refundStatus { get; set; }

      public string role { get; set; }

      public string sellerId { get; set; }

      public string shipType { get; set; }

      public string statusDesc { get; set; }

      public string statusTips { get; set; }

      public List<string> topContactInfo { get; set; }

      public string totalPrice { get; set; }

      public string updateTime { get; set; }
    }

    public class hexiaorootResult
    {
      public List<Model.hexiaorootOrderList> orderList { get; set; }

      public string total { get; set; }
    }

    public class hexiaoroot
    {
      public Model.hexiaorootStatus status { get; set; }

      public Model.hexiaorootResult result { get; set; }
    }

    public class RiskInfo
    {
      public string description { get; set; }

      public string riskLevel { get; set; }
    }

    public class 微店_perResult
    {
      public Model.AliPay aliPay { get; set; }

      public string instCode { get; set; }

      public Model.PayResult payResult { get; set; }

      public Model.RiskInfo riskInfo { get; set; }
    }

    public class 微店_per
    {
      public Model.微店_perStatus status { get; set; }

      public Model.微店_perResult result { get; set; }
    }

    public class 微店_odBuyer1
    {
      public string buyer_id { get; set; }

      public string eat_in_table_name { get; set; }

      public int address_id { get; set; }
    }

    public class 微店_odBuyer
    {
      public string buyer_id { get; set; }

      public string eat_in_table_name { get; set; }

      public int self_address_id { get; set; }

      public string buyer_telephone { get; set; }

      public string buyer_name { get; set; }
    }

    public class 微店_odExtend
    {
      public string actId { get; set; }
    }

    public class 微店_odItem_convey_info
    {
    }

    public class 微店_odItem_list
    {
      public string item_id { get; set; }

      public int quantity { get; set; }

      public string item_sku_id { get; set; }

      public string ori_price { get; set; }

      public string price { get; set; }

      public Model.微店_odExtend extend { get; set; }

      public int price_type { get; set; }

      public List<object> discount_list { get; set; }

      public Model.微店_odItem_convey_info item_convey_info { get; set; }
    }

    public class 微店_odShop_list
    {
      public string shop_id { get; set; }

      public string f_shop_id { get; set; }

      public string sup_id { get; set; }

      public List<Model.微店_odItem_list> item_list { get; set; }

      public int order_type { get; set; }

      public string ori_price { get; set; }

      public string price { get; set; }

      public string express_fee { get; set; }

      public string express_type { get; set; }

      public List<Model.vv> discount_list { get; set; }

      public List<Model.Invalid_item_list> invalid_item_list { get; set; }
    }

    public class vv
    {
      public string id { get; set; }
    }

    public class 微店_odShop_list1
    {
      public string shop_id { get; set; }

      public string f_shop_id { get; set; }

      public string sup_id { get; set; }

      public List<Model.微店_odItem_list> item_list { get; set; }

      public int order_type { get; set; }

      public string ori_price { get; set; }

      public string price { get; set; }

      public string express_fee { get; set; }

      public int express_type { get; set; }

      public List<Model.vv> discount_list { get; set; }

      public List<Model.Invalid_item_list> invalid_item_list { get; set; }
    }

    public class Item_convey_info
    {
    }

    public class Invalid_item_list
    {
      public List<string> ahead_icon_list { get; set; }

      public Model.Item_convey_info item_convey_info { get; set; }

      public string item_head_img { get; set; }

      public string item_id { get; set; }

      public string item_name { get; set; }

      public string item_sku_id { get; set; }

      public string price { get; set; }

      public string price_type { get; set; }

      public int quantity { get; set; }

      public string reason { get; set; }

      public int shop_id { get; set; }

      public string sku_name { get; set; }

      public int status { get; set; }
    }

    public class 微店_od
    {
      public string channel { get; set; }

      public string source_id { get; set; }

      public string q_pv_id { get; set; }

      public int biz_type { get; set; }

      public Model.微店_odBuyer buyer { get; set; }

      public List<Model.微店_odShop_list> shop_list { get; set; }

      public int deliver_type { get; set; }

      public int is_no_ship_addr { get; set; }

      public string total_pay_price { get; set; }

      public string total_vjifen { get; set; }

      public string wfr { get; set; }

      public string appid { get; set; }

      public List<object> discount_list { get; set; }

      public List<object> invalid_shop_list { get; set; }

      public int pay_type { get; set; }
    }

    public class 微店_od1
    {
      public string channel { get; set; }

      public string source_id { get; set; }

      public string q_pv_id { get; set; }

      public int biz_type { get; set; }

      public Model.微店_odBuyer1 buyer { get; set; }

      public List<Model.微店_odShop_list1> shop_list { get; set; }

      public int deliver_type { get; set; }

      public int is_no_ship_addr { get; set; }

      public string total_pay_price { get; set; }

      public string total_vjifen { get; set; }

      public string wfr { get; set; }

      public string appid { get; set; }

      public List<object> discount_list { get; set; }

      public List<object> invalid_shop_list { get; set; }

      public int pay_type { get; set; }
    }

    public class wdinfo
    {
      public string phone { get; set; }

      public string cookie { get; set; }

      public string type { get; set; }

      public string status { get; set; }

      public string payurl { get; set; }

      public string addres { get; set; }

      public string password { get; set; }
    }

    public class KD_Status
    {
      public string code { get; set; }

      public string message { get; set; }

      public string description { get; set; }
    }

    public class KD_Common_express
    {
      public string default_express { get; set; }

      public string express_code { get; set; }

      public string express_company { get; set; }

      public string id { get; set; }
    }

    public class KD_Result
    {
      public List<Model.KD_Common_express> common_express { get; set; }
    }

    public class KD_Root
    {
      public Model.KD_Status status { get; set; }

      public Model.KD_Result result { get; set; }
    }
  }
}
