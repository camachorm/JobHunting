"use strict";
app.controller("weatherController",
    [
        "$scope", "weatherService", function ($scope, weatherService) {
            weatherService.getCities().then(function(response) {
                $scope.cities = response.data;
            });
            $scope.selectedCity = {};
            $scope.selectedCityForecast = {};

            $scope.ShowCityData = function() {
                return Object.keys($scope.selectedCityForecast).length <= 0;
            }

            $scope.SelectCity = function () {
                $scope.selectedCityForecast = {};
                weatherService.getForecast($scope.selectedCity.Id).then(function(response) {
                    $scope.selectedCityForecast = response.data;
                });
            };
        }
    ]);