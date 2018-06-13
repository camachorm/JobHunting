"use strict";
app.factory("weatherService",
    [
        "$http",
        function ($http) {

            var serviceBase = "http://localhost:64716/api/";
            var accountsServiceFactory = {};

            var _getCities = function () {
                return $http.get(serviceBase + "GetCityIds");
            }

            var _getForecast = function (cityId) {
                return $http.get(serviceBase + "GetForecast?cityId=" + cityId);
            }

            accountsServiceFactory.getCities = _getCities;
            accountsServiceFactory.getForecast = _getForecast;

            return accountsServiceFactory;

        }
    ]);