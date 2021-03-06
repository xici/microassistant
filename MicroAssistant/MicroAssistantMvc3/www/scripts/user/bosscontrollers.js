﻿function UserBossMainCtrl($scope, $http, $location) {
    $scope.funnelTimeType = 1;
    $scope.funnelTimeTypeLabel = '本月';
    $scope.oppTimeType = 1;
    $scope.oppTimeTypeLabel = '本月';
    $scope.financeTimeType = 1;
    $scope.financeTimeTypeLabel = '本月';
    $scope.salesFunnelList = [];
    $scope.salesOppList = [];
    $scope.salesFinanceList = [];

    $scope.setFunnelTimeType = function (timeType) {
        $scope.funnelTimeType = timeType;
        switch (timeType)
        {
            case 1:
                $scope.funnelTimeTypeLabel = '本月';
                break;
            case 2:
                $scope.funnelTimeTypeLabel = '本季度';
                break;
            case 3:
                $scope.funnelTimeTypeLabel = '本年度';
                break;
        }
        $scope.loadSalesFunnel();
    };

    $scope.setOppTimeType = function (timeType) {
        $scope.oppTimeType = timeType;
        switch (timeType) {
            case 1:
                $scope.oppTimeTypeLabel = '本月';
                break;
            case 2:
                $scope.oppTimeTypeLabel = '本季度';
                break;
            case 3:
                $scope.oppTimeTypeLabel = '本年度';
                break;
        }
        $scope.loadSalesOpp();
    };
    $scope.setFinanceTimeType = function (timeType) {
        $scope.financeTimeType = timeType;
        switch (timeType) {
            case 1:
                $scope.financeTimeTypeLabel = '本月';
                break;
            case 2:
                $scope.financeTimeTypeLabel = '本季度';
                break;
            case 3:
                $scope.financeTimeTypeLabel = '本年度';
                break;
        }
        $scope.loadSalesFinance();
    };

    $scope.displaySalesFunnel = function (sales) {
        var totalHeight = 326;
        var total = 0;
        var colors = ['#F09B2D', '#7AC0A8', '#E74C3C', '#95A5A6', '#16A085'];
        var titles = ['销售机会', '初步接洽', '多次拜访', '报价', '签订合同'];
        var labelTop = [60, 110, 160, 210, 250];

        for (var i = 0; i < sales.length; i++) {
            total += sales[i];
            var node = {
                style: { background: colors[i], height: 0 },
                labelStyle: { background: colors[i] },
                top: 0,
                labelTop: labelTop[i],
                title: titles[i],
                data: sales[i]
            };
            if ($scope.salesFunnelList[i]) {
                $scope.salesFunnelList[i] = node;
            }
            else {
                $scope.salesFunnelList.push(node);
            }
        }

        total = total <= 0 ? 1 : total;

        var brotherHeight = 16;
        for (var i = 0; i < sales.length; i++) {
            var height = Math.floor((sales[i] / total) * totalHeight);
            $scope.salesFunnelList[i].style.height = height + 'px';
            $scope.salesFunnelList[i].top = Math.floor(brotherHeight + height / 2);
            brotherHeight += height;
        }
    }

    $scope.loadSalesFunnel = function () {
        $http.post($sitecore.urls["salesSalesReport"], { timeType: $scope.funnelTimeType }).success(function (data) {
            console.log(data);
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            else {
                $scope.displaySalesFunnel(data.Data);
            }
        }).
        error(function (data, status, headers, config) {

        });
    }

    $scope.salesFunnel = function () {
        var sales = [0, 0, 0, 0, 1];
        $scope.displaySalesFunnel(sales);
        $scope.loadSalesFunnel();
        
    }
    $scope.salesFunnel();

    //jQuery(document).ready(function () {
    //    for (var i = 0; i < $scope.salesFunnelList.length; i++) {
    //        $('#salesLines').KolaLineDraw({
    //            Width: 200,
    //            Height: 350,
    //            LineColor: '#666',
    //            StartPointLeft: 0,
    //            StartPointTop: $scope.salesFunnelList[i].top,
    //            EndPointLeft: 200,
    //            EndPointTop: $scope.salesFunnelList[i].labelTop
    //        });
    //    }
    //});

    $scope.histogram = function (month, ione, itwo, maxTotal, totalHeight, totalLabelHeight) {

        totalHeight = totalHeight - totalLabelHeight;
        maxTotal = maxTotal <= 0 ? 1 : maxTotal;
        var ioneHeight = Math.floor((ione / maxTotal) * totalHeight);
        var itwoHeight = Math.floor((itwo / maxTotal) * totalHeight);
        var theight = totalHeight - ioneHeight - itwoHeight + totalLabelHeight;

        var ioneStyle = { height: ioneHeight + 'px', 'line-height': ioneHeight + 'px', 'overflow': 'hidden' };
        var itwoStyle = { height: itwoHeight + 'px', 'line-height': itwoHeight + 'px', 'overflow': 'hidden' };
        var totalStyle = { height: theight + 'px' };
        var totalSpaceStyle = { height: (theight > totalLabelHeight ? theight - totalLabelHeight : 0) + 'px' };

        return {
            month: month,
            total: ione + itwo,
            totalStyle: totalStyle,
            totalSpaceStyle: totalSpaceStyle,
            ione: ione,
            ioneStyle: ioneStyle,
            itwo: itwo,
            itwoStyle: itwoStyle
        };
    }

    $scope.salesOppShowPage = 1;
    $scope.salesOppPageCount = 1;
    $scope.salesOppBoxScroll = function (pageAdd) {
        $scope.salesOppShowPage += pageAdd;
        var left = ($scope.salesOppShowPage - 1) * 420;
        $('#salesOppListBox').animate({ 'scrollLeft': left });
    };

    $scope.displaySalesOpp = function (sales)
    {
        $scope.salesOppShowPage = 1;
        $scope.salesOppPageCount = Math.ceil(sales.length / 3);
        $('#salesOppListBox').animate({ 'scrollLeft': 0 });
        var totalHeight = 300;
        var maxTotal = 0;
        var list = [];
        if (sales.length == 1) {
            list.push({});
        }
        for (var i = 0; i < sales.length; i++) {
            var item = sales[i];
            if (item.newc + item.old > maxTotal)
                maxTotal = item.newc + item.old;
        }

        for (var i = 0; i < sales.length; i++) {
            var item = sales[i];
            var node = $scope.histogram(item.month,item.newc, item.old, maxTotal, totalHeight, 20);
            list.push(node);
        }
        if (list.length < 3) {
            list.push({});
        }
        console.log(list);
        $scope.salesOppList = angular.copy(list);
    }

    $scope.loadSalesOpp = function ()
    {
        $http.post($sitecore.urls["salesSalesOppReport"], { timeType: $scope.oppTimeType }).success(function (data) {
            console.log(data);
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            else {
                $scope.displaySalesOpp(data.Data);
            }
        }).
        error(function (data, status, headers, config) {

        });
    }
   
    $scope.salesOpp = function () {
        
        var sales = [{ newc: 100, old: 300 }, { newc: 130, old: 1000 }, { newc: 200, old: 150 }];

        $scope.displaySalesOpp(sales);
        $scope.loadSalesOpp();

    };
    $scope.salesOpp();


    $scope.salesFinanceShowPage = 1;
    $scope.salesFinancePageCount = 1;
    $scope.salesFinanceBoxScroll = function (pageAdd) {
        $scope.salesFinanceShowPage += pageAdd;
        var left = ($scope.salesFinanceShowPage - 1) * 750;
        $('#salesFinanceListBox').animate({ 'scrollLeft': left });
    };
    $scope.displaySalesFinance = function (sales) {
        $scope.salesFinanceShowPage = 1;
        $scope.salesFinancePageCount = Math.ceil(sales.length / 3);
        $('#salesFinanceListBox').animate({ 'scrollLeft': 0 });
        var totalHeight = 300;
        var maxTotal = 0;
        var list = [];
        if (sales.length == 1) {
            list.push({ pay: {}, receive: {}});
        }
        for (var i = 0; i < sales.length; i++) {
            var item = sales[i];
            if (item.paydone + item.notpay > maxTotal)
                maxTotal = item.paydone + item.notpay;

            if (item.received + item.notreceive > maxTotal)
                maxTotal = item.received + item.notreceive;
        }

        for (var i = 0; i < sales.length; i++) {
            var item = sales[i];
            var node = {
                pay: $scope.histogram(item.month, item.paydone, item.notpay, maxTotal, totalHeight, 20),
                receive: $scope.histogram(item.month, item.received, item.notreceive, maxTotal, totalHeight, 20)
            };
            list.push(node);
        }
        if (list.length < 3) {
            list.push({ pay: {}, receive: {} });
        }
        console.log(list);
        $scope.salesFinanceList = angular.copy(list);
    }

    $scope.loadSalesFinance = function () {
        $http.post($sitecore.urls["salesFinanceReport"], { timeType: $scope.financeTimeType }).success(function (data) {
            console.log(data);
            if (data.Error) {
                alert(data.ErrorMessage);
            }
            else {
                $scope.displaySalesFinance(data.Data);
            }
        }).
        error(function (data, status, headers, config) {

        });
    }

    $scope.salesFinance = function () {
       
        
        var sales = [{ month: 5, paydone: 100, notpay: 300, received: 200, notreceive: 230 },
            { month: 6, paydone: 500, notpay: 200, received: 240, notreceive: 530 },
        { month: 7, paydone: 140, notpay: 380, received: 80, notreceive: 330 }];

        $scope.displaySalesFinance(sales);
        $scope.loadSalesFinance();

    };
    $scope.salesFinance();
}