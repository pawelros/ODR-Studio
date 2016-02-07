var studioApp = angular.module('studioApp', []);
studioApp.controller('playerController', function ($scope, $http, $interval) {
    $scope.status = {"isOnline":true,
    "isPlaying":true,
    "isStopped":false,
    "isPaused":false,
    "trackName":"OneRepublic - Native.flac",
    "trackTime":47,
    "trackLength":4000,
    "trackPosition":0.411879617348313};
   
    $scope.counter = 0;

            var getStatus = function () {
                 $http.get("http://localhost:5001/api/player").then(function(response) {
                    $scope.status = response.data;});
            }

            $interval(getStatus, 250); 

});


