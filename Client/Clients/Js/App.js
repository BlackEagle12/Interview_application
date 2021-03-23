var Myapp = angular.module('Interview_application', ['ui.router']);

Myapp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('Home', {
            url: '/Application_form',
            templateUrl: '../Html/ApplicationForm.html',
            controller: 'Application_Form_Controller'
        })
        .state('View', {
            url: '/View_application',
            templateUrl: '../Html/ViewApplication.html',
            controller: 'Application_Form_Controller'
        })
        .state('Update', {
            url: '/Application_form/edit/:id',
            templateUrl: '../Html/ApplicationForm.html',
            controller: 'Application_Form_Controller'
        })
        .state('Admin', {
            url: '/admin',
            templateUrl: '../Html/Admin.html',
            controller: 'Admin_Controller'
        })
        .state('AdminLogin', {
            url: '/admin/login',
            templateUrl: '../Html/AdminLogin.html',
            controller: 'Admin_Controller'
        })
        .state('Admin.Home', {
            url: '/home',
            templateUrl: '../Html/AdminHome.html',
            controller: 'Admin_Controller'
        })
        .state('Admin.Profile', {
            url: '/profile',
            templateUrl: '../Html/AdminProfile.html',
            controller: 'Admin_Controller'
        })
        .state('AdminView', {
            url: '',
            templateUrl: '',
            controller: ''
        })
    $urlRouterProvider.otherwise('/Application_form');
}]);
