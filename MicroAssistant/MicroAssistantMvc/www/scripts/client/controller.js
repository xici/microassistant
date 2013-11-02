﻿function ClientMainCtrl($scope, $routeParams, $http, $location) {
    $scope.sorts = $routeParams.sorts;
    if (!$scope.sorts)
        $scope.sorts = "enterprise";//企业
    $scope.loadCurrentSortList = function () {
        switch ($scope.sorts) {
            case 'enterprise'://获取企业用户
                $http.post($sitecore.urls["SearchCustomerEntByOwnerId"]).success(function (data) {
                    $scope.enterpriseclients = data.Data.Items;
                }).error(function (data, status, headers, config) {
                    $scope.enterpriseclients = [];
                });
                break;
            case 'personal'://获取个人用户
                $http.post($sitecore.urls["SearchCustomerPrivByOwnerId"]).success(function (data) {
                    $scope.personalclients = data.Data.Items;
                }).error(function (data, status, headers, config) {
                    $scope.personalclients = [];
                });
                break;
        }
    };
    $scope.loadCurrentSortList();
    $scope.ShowAddEnterpriseForm = function () {
        $scope.$broadcast('EventAddEnterprise', this.enterpriseclientItem);
    };
    $scope.ShowAddPersonalForm = function () {
        $scope.$broadcast('EventAddPersonal', this);
    }
}
function AddClientCtrl($scope, $routeParams, $http, $location) {
    switch ($scope.sorts) {
        case 'enterprise':
            var formdata;
            $scope.$on("EventAddEnterprise", function (event, data) {
                formdata = data;
                if (form) {
                    $scope.EnterpriseItem = angular.copy(formdata);

                }
                else {
                    $scope.EnterpriseItem = { CustomerEntId: 0 };
                }
                //if (product) {
                //    $scope.EditProduct = angular.copy(product);
                //    if (angular.isArray($scope.PTypes)) {
                //        angular.forEach($scope.PTypes, function (value) {
                //            if (value.PTypeId == product.PTypeId)
                //                $scope.EditProduct.PType = value;
                //        });
                //    }
                //}
               
                $("#AddEnterpriseBox").modal('show');
            });
            $scope.AddEnterpriseSubmit = function () {
                if ($scope.AddEnterpriseForm.$valid) {
                    $scope.showerror = false;
                    $http.post($sitecore.urls["AddOrUpdateEnterPriseClient"], { entid: $scope.EnterpriseItem.CustomerEntId, entName: $scope.EnterpriseItem.EntName, industy: $scope.EnterpriseItem.Industy, contactUsername: $scope.EnterpriseItem.ContactUsername, contactMobile: $scope.EnterpriseItem.ContactMobile, phone: $scope.EnterpriseItem.ContactPhone, email: $scope.EnterpriseItem.ContactEmail, qq: '', address: $scope.EnterpriseItem.Address, Detail: $scope.EnterpriseItem.Detail }).success(function (data) {
                        if (data.Error) {
                            alert(data.ErrorMessage);
                        }
                        else {
                            //if (from && angular.isArray(from.stores)) {
                            //    $scope.AddedPurchase.PId = data.Id;
                            //    from.stores.push(angular.copy($scope.AddedPurchase));
                            //}
                            //if (from && from.product) {
                            //    from.product.StockCount = (from.product.StockCount || 0) + $scope.AddedPurchase.PNum;
                            //}
                            $("#AddEnterpriseBox").modal('hide');
                        }
                    }).
                    error(function (data, status, headers, config) {
                        //$scope.product = {};
                    });
                }
                else {
                    $scope.showerror = true;
                }
            };
            break;
        case 'personal':
            var form;
            $scope.$on("EventAddPersonal", function (event, formscope) {
                $("#AddPersonalBox").modal('show');
            });
            $scope.AddPersonalSumbit = function () {
                if ($scope.AddentPersonalForm.$valid) {
                    $scope.showerror = false;
                    $http.post($sitecore.urls["productAddStores"], { pid: from.product.PId, num: $scope.AddedPurchase.PNum, price: $scope.AddedPurchase.Price }).success(function (data) {
                        
                        if (data.Error) {
                            alert(data.ErrorMessage);
                        }
                        else {
                            //if (from && angular.isArray(from.stores)) {
                            //    $scope.AddedPurchase.PId = data.Id;
                            //    from.stores.push(angular.copy($scope.AddedPurchase));
                            //}
                            //if (from && from.product) {
                            //    from.product.StockCount = (from.product.StockCount || 0) + $scope.AddedPurchase.PNum;
                            //}
                            //$('#addPurchaseModal').modal('hide');
                        }
                    }).
                    error(function (data, status, headers, config) {
                        //$scope.product = {};
                    });
                }
                else {
                    $scope.showerror = true;
                }
            };
            break;
        default:
            var form;
            $scope.$on("EventAddEnterprise", function (event, formscope) {
                form = formscope;
                $("#AddEnterpriseBox").modal('show');
            });
            $scope.AddEnterpriseSubmit = function () {
                if ($scope.AddentEnterpriseForm.$valid) {
                    $scope.showerror = false;
                    $http.post($sitecore.urls["AddOrUpdateEnterPriseClient"], { pid: from.product.PId, num: $scope.AddedPurchase.PNum, price: $scope.AddedPurchase.Price }).success(function (data) {
                      
                        if (data.Error) {
                            alert(data.ErrorMessage);
                        }
                        else {
                            //if (from && angular.isArray(from.stores)) {
                            //    $scope.AddedPurchase.PId = data.Id;
                            //    from.stores.push(angular.copy($scope.AddedPurchase));
                            //}
                            //if (from && from.product) {
                            //    from.product.StockCount = (from.product.StockCount || 0) + $scope.AddedPurchase.PNum;
                            //}
                            //$('#addPurchaseModal').modal('hide');
                        }
                    }).
                    error(function (data, status, headers, config) {
                        //$scope.product = {};
                    });
                }
                else {
                    $scope.showerror = true;
                }
            };
            break;
    }


};