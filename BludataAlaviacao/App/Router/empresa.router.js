app.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/empresa/add', {
        templateUrl: '/App/View/Empresa/cad.html',
        controller: 'EmpresaCtrl'
    }).when('/empresa/:id', {
        templateUrl: '/App/View/Empresa/cad.html',
        controller: 'EmpresaCtrl'
    }).when('/empresa', {
        templateUrl: '/App/View/Empresa/index.html',
        controller: 'EmpresaCtrl'
    });
});