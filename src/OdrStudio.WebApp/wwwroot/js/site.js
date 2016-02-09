var studioApp = angular.module('studioApp', []);
studioApp.controller('playerController', function ($scope, $http, $interval) {
    
    var apiUrl = "http://localhost:5001/api/";

    var defaultStatus = {
    "isApiOnline":false,
    "isOnline":false,
    "isPlaying":false,
    "isStopped":false,
    "isPaused":false,
    "trackName":"",
    "trackTime":0,
    "trackLength":0,
    "trackPosition":0,
    "motSlideShowUrls" : null};
    
    $scope.status = defaultStatus;

    $scope.Math = window.Math;

            var getStatus = function () {
                 $http.get(apiUrl + "player/status").then(function(response) {
                    $scope.status = response.data;
                    $scope.status.isApiOnline = true;

                    if($scope.status.motSlideShowUrls){
                        var index;
                        for (index = 0; index < $scope.status.motSlideShowUrls.length; ++index) {
                            $scope.status.motSlideShowUrls[index] = apiUrl + $scope.status.motSlideShowUrls[index];
                        }
                    }

                    }, function(response){
                        $scope.status = defaultStatus;
                    });
            }

            $interval(getStatus, 250); 
});


