app.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', {
        templateUrl: '/App/View/home.html',
        controller: 'HomeCtrl'
    });
});