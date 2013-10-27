var $sitecore = $sitecore || {};

$sitecore.urls = $sitecore.urls || {};
$sitecore.urls.base = "";
$sitecore.urls.add = function(name,url){
	$sitecore.urls[name] = $sitecore.urls.base + url +"?timestamp="+ new Date().getTime();
};
$sitecore.urls.add("userLogin", "/UserManagement/User/Login"); 
$sitecore.urls.add("userRegister", "/UserManagement/User/UserRegister"); 
$sitecore.urls.add("enterpriseRegister", "/UserManagement/User/EntRegister");

$sitecore.urls.add("productCat", "/ProductManagement/Production/SearchProductTypeListByEntID");
$sitecore.urls.add("productAddCat", "/ProductManagement/Production/AddProductionType"); 
$sitecore.urls.add("productList", "/ProductManagement/Production/SearchProductionSByType");
$sitecore.urls.add("productDetail", "/ProductManagement/Production/GetProductInfoByPID");
$sitecore.urls.add("productAdd", "/ProductManagement/Production/AddProduction");
$sitecore.urls.add("productUpdate", "/ProductManagement/Production/UpateProduction");
$sitecore.urls.add("productStoresList", "/ProductManagement/Production/SearchProductonDetailList"); 
$sitecore.urls.add("productAddStores", "/ProductManagement/Production/AddProductonDetail");

$sitecore.urls.add("salesAddChance", "/MarketingManagement/Marketing/AddMarketingChance"); 
$sitecore.urls.add("salesChanceList", "/MarketingManagement/Marketing/SearchMarketingList"); 
$sitecore.urls.add("salesChanceVisitsList", "/MarketingManagement/Marketing/GetVisitInfo");
$sitecore.urls.add("salesAddChanceVisits", "/MarketingManagement/Marketing/ToVisit"); 
$sitecore.urls.add("salesRateChange", "/MarketingManagement/Marketing/EditCustomerRate");
$sitecore.urls.add("salesChanceVisitList", "/MarketingManagement/Marketing/SearchVisitInfoList");

$sitecore.urls.add("receivablesfinanceList", "/FinancialManagement/Financial/SearchReceivables");//根据企业ID获取应收款列表 返回 应收款列表
$sitecore.urls.add("payablesfinanceList", "/FinancialManagement/Financial/SearchPayablesByEID");//根据企业ID获取应付款列表 返回 应付款列表
$sitecore.urls.add("receivablesDetail", "/FinancialManagement/Financial/GetHowToPayByEID");//应收款详情
$sitecore.urls.add("makeSureTimesReceivable", "/FinancialManagement/Financial/ConfirmReceived");//确认收款

