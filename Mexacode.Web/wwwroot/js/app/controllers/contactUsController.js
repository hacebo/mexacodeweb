'use strict';
angular.module('mexacodeApp')
    .controller('contactUsController', contactUsController);

contactUsController.$inject = ['$scope', 'contactUsServices'];

function contactUsController($scope, services) {
    $scope.sendEmail = function () {
        if ($scope.contactForm.$valid) {
            var email = $scope.contactForm;
            services.sendEmail(email).then(function () {
                swal("Mensaje Enviado!", "Gracias por ponerse en contacto con nosotros en breve uno de nuestros asesores lo contactara!", "success");
            }, function () {
                swal("Error", "Ocurrio un error al guardar su informacion por favor intente mas tarde.", "error");
            });
        };
    }
};
