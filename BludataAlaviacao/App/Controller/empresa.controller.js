app.controller('EmpresaCtrl', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();

    $scope.startForm = () => $scope.ufs = [{
        'uf': 'PR',
        'nome': 'Paraná'
    },
    {
        'uf': 'SC',
        'nome': 'Santa Catarina'
    }];
});