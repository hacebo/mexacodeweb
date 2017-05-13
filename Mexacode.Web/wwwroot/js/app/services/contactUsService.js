
'use strict';
angular.module('mexacodeApp')
    .service('contactUsServices', contactUsServices);

contactUsServices.$inject = ['Restangular'];
function contactUsServices(Restangular) {
    this.sendEmail = function (email) {
        return Restangular.one('api/ContactUs').post('', email);
    };
};
