app.filter('filtroFornecedor', function () {
    console.log("Entrou aqui");
    return function (input, condition, third) {

        if (!condition) {
            return input;
        } else {
            var output = [];
            input.forEach(item => {
                var inclui = true;

                if (!!condition.nomeFornecedor && condition.nomeFornecedor != '') {
                    inclui = inclui && item.nomeFornecedor.toUpperCase().includes(condition.nomeFornecedor.toUpperCase());
                } else {
                    inclui = inclui && true;
                }

                condition.empresa = condition.empresa || {};
                if (!!condition.empresa.nomeEmpresa && condition.empresa.nomeEmpresa != '') {
                    inclui = inclui && item.empresa.nomeEmpresa.toUpperCase().includes(condition.empresa.nomeEmpresa.toUpperCase());
                } else {
                    inclui = inclui && true;
                }

                var pessoa = item.fornecedorPessoaFisica || item.fornecedorPessoaJuridica;
                var documento = pessoa.cpfFornecedorPessoaFisica || pessoa.cnpjFornecedorPessoaJuridica;

                if (!!condition.documento && condition.documento != '') {
                    inclui = inclui && documento.toUpperCase().includes(condition.documento.toUpperCase());
                } else {
                    inclui = inclui && true;
                }

                if (!!condition.dataCadastroFornecedor && condition.dataCadastroFornecedor != '') {
                    inclui = inclui && item.dataCadastroFornecedor.toUpperCase().includes(condition.dataCadastroFornecedor.toUpperCase());
                } else {
                    inclui = inclui && true;
                }


                if (inclui) {
                    output.push(item);
                }
            });

            return output;
        }
    };
}).controller('FornecedorCtrl', function ($rootScope, $scope, $location, $routeParams, fornecedorService, empresaService) {
    $rootScope.activetab = $location.path();

    $scope.startForm = () => {
        if (!!$routeParams.id) {
            fornecedorService.get($routeParams.id, res => {
                $scope.model = res.data;
                $scope.model.isPessoaFisica = $scope.model.fornecedorPessoaFisica != null;
                $scope.model.isPessoaJuridica = !$scope.model.isPessoaFisica;

                if ($scope.model.isPessoaFisica) {
                    /**
                     * @type {String}
                     * */
                    var strDate = $scope.model.fornecedorPessoaFisica.dataNascimentoFornecedorPessoaFisica;
                    $scope.model.fornecedorPessoaFisica.dataNascimentoFornecedorPessoaFisica = strDate.substring(0, 10);
                }
            });
        } else {
            $scope.limpar();
        }

        empresaService.getAll(res => $scope.empresas = res.data);
    };

    $scope.startGrid = () => {
        fornecedorService.getAll(res => {
            $scope.fornecedores = res.data;
            console.log($scope.fornecedores);
        });
    };

    $scope.removeTelefone = tel => {
        var telefones = [];
        $scope.model.telefones.forEach(item => {
            if (item != tel) {
                telefones.push(item);
            }
        });

        $scope.model.telefones = telefones;
    };

    $scope.addTelefone = addtelefone => {
        if (addtelefone != null && addtelefone !== "") {
            $scope.model.telefones.push({
                idFornecedor: $scope.model.idFornecedor,
                numeroTelefone: addtelefone
            });
            delete $scope.telefone;
        }
    };

    $scope.limpar = () => {
        $scope.model = { isPessoaFisica: true, telefones: [] };
    };

    $scope.isPessoaFisicaMenorPR = model => {
        var isPFPR = model.empresa.ufEmpresa.toUpperCase() == 'PR' && model.isPessoaFisica;

        if (!isPFPR)
            return false;

        var hoje = new Date();
        var maioridade = new Date(hoje.getFullYear() - 18, hoje.getMonth(), hoje.getDay(), 0, 0, 0, 0);
        var dtNasc = new Date(model.fornecedorPessoaFisica.dataNascimentoFornecedorPessoaFisica);

        console.log(maioridade, dtNasc, dtNasc >= maioridade);

        return dtNasc > maioridade;
    };

    $scope.alterarEmpresa = idEmpresa => $scope.empresas.forEach(e => {
        if (e.idEmpresa == idEmpresa) {
            $scope.model.empresa = e;
        }
    });

    $scope.enviar = (form, model) => {

        if (!$scope.isPessoaFisicaMenorPR(model)) {
            if (!!model.idFornecedor) {
                fornecedorService.put(model.idFornecedor, model, res => console.log(res));
            } else {
                fornecedorService.post(model, res => $scope.limpar());
            }
        } else {
            $rootScope.modal = {
                title: 'Aviso!',
                danger: true,
                show: true,
                message: 'Não é permitido cadastrar pessoa menor de idade para esta empresa.'
            };
        }
    };

    $scope.excluir = (id, nome) => {
        if (confirm(`Gostaria de excluir o fornecedor ${nome}?`)) {
            fornecedorService.delete(id, res => $scope.startGrid());
        }
    };
});