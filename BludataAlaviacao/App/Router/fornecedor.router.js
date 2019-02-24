app.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/fornecedor/add', {
        templateUrl: '/App/View/Fornecedor/cad.html',
        controller: 'FornecedorCtrl'
    }).when('/fornecedor/:id', {
        templateUrl: '/App/View/Fornecedor/cad.html',
        controller: 'FornecedorCtrl'
    }).when('/fornecedor', {
        templateUrl: '/App/View/Fornecedor/index.html',
        controller: 'FornecedorCtrl'
    });
});