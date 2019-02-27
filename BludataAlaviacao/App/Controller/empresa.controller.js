app.controller('EmpresaCtrl', function ($rootScope, $scope, $location, empresaService) {
    $rootScope.activetab = $location.path();

    $scope.startForm = () => $scope.ufs = [{
        'uf': 'PR',
        'nome': 'Paraná'
    },
    {
        'uf': 'SC',
        'nome': 'Santa Catarina'
        }];

    $scope.enviar = (form, model) => empresaService.post(model, res => $rootScope.modal = {
        show: true,
        message: res
    });
});