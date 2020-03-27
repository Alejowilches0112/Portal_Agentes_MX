
app.controller('PerfilarController', function ($scope, BayportService, $filter, $location, $window) {
    $scope.formulario = {};
    $scope.Entidad = {desprendible:'1'};
    $scope.ConvenioID = null;
    $scope.PagaduriaID = null;
    $scope.ConvenioList = null;
    $scope.PagaduriaList = null;
    $scope.tipCreditoID = null;
    $scope.lineCreditoID = null;
    $scope.entityID = null;
    $scope.cartera = [];
    $scope.formulario.numDoc=0;
    $scope.ConvenioTextToShow = "SELECCIONE CONVENIO";
    $scope.PagaduriaTextToShow = "SELECCIONE PAGADURIA";
    $scope.TipoContratoTextToShow = "SELECCIONE TIPO CONTRATO";
    $scope.RangoTextToShow = "SELECCIONE RANGO";
    $scope.EntidadTextToShow = "SELECCIONE ENTIDAD";
    $scope.Result = "";
    $scope.mostrartabla = false;

    BayportService.GetConvenio().then(function (d) {     
        $scope.ConvenioList = d.data;   
    },
    function (error) {
        alert('Error!');    
    });
    
    $scope.GetPagaduria = function () {
        $scope.PagaduriaID = null; 
        $scope.PagaduriaList = null;
        $scope.formulario.convenio = $scope.ConvenioID;
        $scope.PagaduriaTextToShow = "Por favor Espere..."; 

       
        BayportService.GetPagaduria($scope.ConvenioID).then(function (d) {
            $scope.PagaduriaList = d.data;
            $scope.PagaduriaTextToShow = "SELECCIONE PAGADURIA";
        }, function (error) {
            alert('Error!');
        });

    };


    $scope.GetTiposContrato = function () {
        $scope.TipoContratoID = null; 
        $scope.TipoContratoList = null; 
        $scope.TipoContratoTextToShow = "Por favor Espere..."; 
        $scope.formulario.pagaduria = $scope.PagaduriaID;
        //Load State 
        BayportService.GetTipoContrato($scope.ConvenioID, $scope.PagaduriaID).then(function (d) {
            $scope.TipoContratoList = d.data;
            $scope.TipoContratoTextToShow = "SELECCIONE TIPO CONTRATO";

        }, function (error) {
            alert('Error!');
        });

    };


    $scope.GetRango = function () {
        $scope.RangoID = null;
        $scope.RangoList = null;
        $scope.RangoTextToShow = "Por favor Espere...";
        $scope.formulario.tipocontrato=$scope.TipoContratoID;
        //Load State 
        BayportService.GetRango($scope.PagaduriaID).then(function (d) {
            $scope.RangoList = d.data;
            $scope.RangoTextToShow = "SELECCIONE RANGO";

        }, function (error) {
            alert('Error!');
        });

    };

 


    BayportService.GetTipCredito().then(function (d) {
        
        $scope.TipCreditoList = d.data;
    },
     function (error) {
         alert('Error!');    
     });

    BayportService.GetLineCredito().then(function (d) {

        $scope.LineCreditoList = d.data;
    },
     function (error) {
         alert('Error!');
     });

    BayportService.GetEntity().then(function (d) {
        $scope.EntityList = d.data;
    },
     function (error) {
         alert('Error!');
     });

    $scope.GuardarEntity = function(){
        if ((($scope.formulario.Entname === undefined) || ($scope.formulario.cuota === undefined) || ($scope.formulario.saldo === undefined))) {
            $scope.formulario = { desprendible: '1' };
            return;
        } else {
            if ($scope.cartera.length < 4) {
                $scope.formulario.descripcion = $filter('filter')($scope.EntityList.purchaseEntityList, { entityCode: $scope.formulario.Entname })[0].entityName;
                $scope.Entidad = {
                    name: $scope.formulario.Entname,
                    descripcion: $scope.formulario.descripcion,
                    cuota: $scope.formulario.cuota,
                    saldo: $scope.formulario.saldo,
                    desprendible: $scope.formulario.desprendible
                };

                $scope.cartera.push($scope.Entidad);
                if ($scope.cartera.length > 0) {
                    $scope.mostrartabla = true;
                    $scope.formulario.Entname=BayportService.GetEntity()[0];
                    $scope.formulario.cuota = undefined;
                    $scope.formulario.saldo = undefined;
                    $scope.formulario.desprendible = '1';
                };

                
            } else {
                alert('Error!');
            }

        };


    };

    $scope.removeItem = function (index) {
        
        $scope.cartera.splice(index, 1);
        if ($scope.cartera.length <= 0) {
            $scope.mostrartabla = false;
        };
    };
    
   
    $scope.procesarFormulario = function (valido) {
       
        if (valido) {
            //esto es solo temporal
            console.log($scope.formulario);
            $scope.formulario.cartera = $scope.cartera;
            BayportService.ProcesarOferta($scope.formulario).then(function (d) {
                $scope.oferta = d.data;
                if (d.data.msg.errorCode !== "200") {
                    alert(d.data.msg.errorMessage);
                }
                else {
                    $window.location.href = d.data.msg.errorMessage ;
                }
                
            }, function (error) {
                $location.path('/Error/?error=505');
            });


        } else {
            alert('Por favor Diligencie formulario correctamente!');

        }
            


    };


});