'use strict';
angular.module('mexacodeApp')
    .controller('contactUsController', contactUsController);

contactUsController.$inject = ['$scope', 'contactUsServices'];
function contactUsController($scope, services) {
    $scope.sendEmail = function () {
        console.log($scope);
    }
};
