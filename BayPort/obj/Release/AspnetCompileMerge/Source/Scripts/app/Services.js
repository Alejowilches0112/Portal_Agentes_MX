
app.factory('BayportService', function ($http) {
    var fac = {};
    fac.GetConvenio = function () {
        return $http.get('/Profile/GetAgreements');
    };
    fac.GetPagaduria = function (id) {
        return $http({
            method: 'POST',
            url: '/Profile/Payables',
            data: { code: id },
            headers: {'Content-Type': 'application/json'}
        });
    };
    fac.GetTipoContrato = function (id1,id2) {
        return $http({
            method: 'POST',
            url: '/Profile/ContractType',
            data: { code1: id1,code2:id2 },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetRango = function (id) {
        return $http({
            method: 'POST',
            url: '/Profile/Range',
            data: { code: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };

    fac.GetTipCredito = function () {
        return $http.get('/Profile/CreditType');
    };
    fac.GetLineCredito = function () {
        return $http.get('/Profile/CreditLine');
    };

    fac.GetEntity = function () {
        return $http.get('/Profile/Entity');
    };


    fac.ProcesarOferta = function (data) {
        return $http({
            method: 'POST',
            url: '/Profile/Process',
            data: { formulario: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };


    return fac;




});