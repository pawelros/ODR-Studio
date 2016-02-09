var studioApp = angular.module('studioApp', []);
studioApp.controller('playerController', function ($scope, $http, $interval) {

    var defaultStatus = {
    "isApiOnline":false,
    "isOnline":false,
    "isPlaying":false,
    "isStopped":false,
    "isPaused":false,
    "trackName":"",
    "trackTime":0,
    "trackLength":0,
    "trackPosition":0};
    
    $scope.status = defaultStatus;

    $scope.Math = window.Math;

            var getStatus = function () {
                 $http.get("http://localhost:5001/api/player/status").then(function(response) {
                    $scope.status = response.data;
                    $scope.status.isApiOnline = true;
                    }, function(response){
                        $scope.status = defaultStatus;
                    });
            }

            $interval(getStatus, 250); 
});


