var studioApp = angular.module('studioApp', []);
studioApp.controller('playerController', function ($scope, $http, $interval) {

    var apiUrl = "http://localhost:5001/api/";
    var previousMotSlideShowUrls = [];
    $scope.newDls = null;

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
    "motSlideShowUrls" : null,
    "dls": null};

    $scope.status = defaultStatus;

    $scope.Math = window.Math;
    
    var putMot = function () {
        $http.put($scope.status.motSlideShowUrls[0], null);
    }

    $scope.updateDls = function() {
        // Deep copy
        var newStatus = jQuery.extend(true, {}, $scope.status);
        newStatus.dls = $scope.newDls;
        $http({
                url: $scope.status.motSlideShowUrls[0],
                method: "PUT",
                data: newStatus
            });
        $scope.newDls = null;
    }

    var getStatus = function () {
            $http.get(apiUrl + "player/status").then(function(response) {
            $scope.status = response.data;
            $scope.status.isApiOnline = true;

            if($scope.status.motSlideShowUrls){
                var index;
                var motChanged = false;
                
                //console.log("====== checking if mot has changed ======");
                
                //console.log("current length: "+$scope.status.motSlideShowUrls.length+" prev length: "+previousMotSlideShowUrls.length);
                if($scope.status.motSlideShowUrls.length !== previousMotSlideShowUrls.length){
                    //console.log("Length differs, mot has changed.")
                    motChanged = true;
                }
                
                for (index = 0; index < $scope.status.motSlideShowUrls.length; ++index) {
                    $scope.status.motSlideShowUrls[index] = apiUrl + $scope.status.motSlideShowUrls[index];

                    if (motChanged === false) {
                        //console.log("Comparing "+index+" element of current and prev: curr: "+ $scope.status.motSlideShowUrls[index]+"prev: "+previousMotSlideShowUrls[index]);
                        if($scope.status.motSlideShowUrls[index] !== previousMotSlideShowUrls[index]){
                            //console.log("Url differs, mot has changed.");
                            motChanged = true;
                        }
                    }
                }

                if(motChanged) {
                    //console.log("Sending PUT "+$scope.status.motSlideShowUrls[0]);
                    previousMotSlideShowUrls = $scope.status.motSlideShowUrls.slice();
                    putMot();
                }
            }

            }, function(response){
                $scope.status = defaultStatus;
            });
    }
    $interval(getStatus, 250); 
});


