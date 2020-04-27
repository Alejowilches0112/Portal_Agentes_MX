app.factory('BayportService', function ($http, $q) {
    var fac = {};
    fac.GetUserOptions = function (ind_menu) {
        return $http({
            method: 'POST',
            url: '/Profile/GetUserOptions',
            data: { ind_menu: ind_menu },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetConvenio = function () {
        return $http.get('/Profile/GetAgreements');
    };
    fac.GetPagaduria = function (id) {
        return $http({
            method: 'POST',
            url: '/Profile/Payables',
            data: { code: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetTipoContrato = function (id1, id2) {
        return $http({
            method: 'POST',
            url: '/Profile/ContractType',
            data: { code1: id1, code2: id2 },
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
    //fac.GetLineCredito = function () {
    //    return $http.get('/Profile/CreditLine');
    //};
    fac.GetLineCredito = function (id) {
        return $http({
            method: 'POST',
            url: '/Profile/CreditLine',
            data: { agreementCode: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetEntity = function () {
        return $http.get('/Profile/Entity');
    };
    fac.ProcesarOferta = function (data, dependencia, producto) {
        return $http({
            method: 'POST',
            url: '/Profile/Process',
            data: { formulario: data, dependencia: dependencia, producto: producto },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.SubirArchivos = function (files) {
        return $http({
            method: 'POST',
            url: '/UpdateExecutive/PostFilesExcecutive',
            headers: { "Content-Type": 'application/json' },
            data: files,
            transformRequest: angular.identity
        });
    };
    fac.aceptarOferta = function (data, data2) {
        return $http({
            method: 'POST',
            url: '/Profile/acceptOffer',
            data: { oferta: data, flag: data2 },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetOferta = function () {
        return $http.get('/Profile/obtainOffer');
    };
    fac.rechazarOferta = function (data) {
        return $http({
            method: 'POST',
            url: '/Profile/rejectOffer',
            data: { oferta: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetRequisitionByClient = function (id) {
        return $http({
            method: 'POST',
            url: '/ReqByClient/GetRequisitionByClient',
            data: { documentID: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetRequisitionByDate = function (fecIni, fecFin) {
        return $http({
            method: 'POST',
            url: '/ReqByDate/GetRequisitionByDate',
            data: { pStartDate: fecIni, pEndDate: fecFin },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetInfoMotor = function (id) {
        return $http({
            method: 'POST',
            url: '/ReqByDate/GetInfoMotor',
            data: { folder: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetLoanHeader = function (id) {
        return $http({
            method: 'POST',
            url: '/ReqByClient/GetLoanHeader',
            data: { folder: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetLoanDetailList = function (id) {
        return $http({
            method: 'POST',
            url: '/ReqByClient/GetLoanDetail',
            data: { folder: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetLoanFiles = function (id) {
        return $http({
            method: 'POST',
            url: '/ReqByDate/GetLoanFiles',
            data: { folder: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.DownloadLoanFiles = function (file) {
        return $http({
            method: 'POST',
            url: '/ReqByDate/DownloadLoanFiles',
            data: { FileName: file },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetCustomerInformation = function (tipoDoc, data) {
        return $http({
            method: 'POST',
            url: '/Profile/GetCustomerInformation',
            data: { DocType: tipoDoc, customerID: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetFolderInformation = function (tipoDoc, data) {
        return $http({
            method: 'POST',
            url: '/Profile/GetFolderInformation',
            data: { DocType: tipoDoc, customerID: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetListDocumentsCustomer = function () {
        return $http.get('/Profile/GetLisDocuments');
    };
    fac.SubirArchivosPerfilamiento = function (files) {
        return $http({
            method: 'POST',
            url: '/Profile/PostFilesCustomer',
            headers: { "Content-Type": undefined },
            data: files,
            transformRequest: angular.identity
        });
    };
    //-----------------------------------------------------------------------------------------------------//
    // Actualizacion Agentes
    fac.GetCivilStatus = function () {
        return $http.get('/UpdateExecutive/GetCivilStatus');
    };
    fac.GetDepartments = function () {
        return $http.get('/UpdateExecutive/GetDepartments');
    };
    fac.GetHousingType = function () {
        return $http.get('/UpdateExecutive/GetHousingType');
    };
    fac.GetAppliedStudies = function () {
        return $http.get('/UpdateExecutive/GetAppliedStudies');
    };
    fac.GetAFP = function () {
        return $http.get('/UpdateExecutive/GetAFP');
    };
    fac.GetARP = function () {
        return $http.get('/UpdateExecutive/GetARP');
    };
    fac.GetEPS = function () {
        return $http.get('/UpdateExecutive/GetEPS');
    };
    fac.GetBanks = function () {
        return $http.get('/UpdateExecutive/GetBanks');
    };
    fac.GetBornCity = function () {
        return $http.get('/UpdateExecutive/GetBornCity');
    };
    fac.GetCancellationCausal = function () {
        return $http.get('/UpdateExecutive/GetCancellationCausal');
    };
    fac.GetCategory = function () {
        return $http.get('/UpdateExecutive/GetCategory');
    };
    fac.GetBranches = function () {
        return $http.get('/UpdateExecutive/GetBranches');
    };
    fac.GetRegionals = function () {
        return $http.get('/UpdateExecutive/GetRegionals');
    };
    fac.GetCoordinators = function () {
        return $http.get('/UpdateExecutive/GetCoordinators');
    };
    fac.GetExecutiveType = function () {
        return $http.get('/UpdateExecutive/GetExecutiveType');
    };
    fac.GetChannelType = function () {
        return $http.get('/UpdateExecutive/GetChannelType');
    };
    fac.GetSalesChannel = function () {
        return $http.get('/UpdateExecutive/GetSalesChannel');
    };
    fac.GetLisDocuments = function () {
        return $http.get('/UpdateExecutive/GetLisDocuments');
    };
    fac.GetCities = function (id) {
        return $http({
            method: 'POST',
            url: '/UpdateExecutive/GetCities',
            data: { departmentID: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetCitiesall = function () {
        return $http.get('/UpdateExecutive/GetCitiesall');
    };
    fac.GetNeighborhood = function (id) {
        return $http({
            method: 'POST',
            url: '/UpdateExecutive/GetNeighborhood',
            data: { municipalityID: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetExecutiveInformation = function () {
        return $http.get('/UpdateExecutive/GetExecutiveInformation');
    };
    fac.ActualizarAsesor = function (data) {
        return $http({
            method: 'POST',
            url: '/UpdateExecutive/UpdateExcecutive',
            data: {
                formulario: data
            },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    // Comisiones
    fac.GetMovesCommissions = function (fecIni, fecFin) {
        return $http({
            method: 'POST',
            url: '/Commissions/GetMovesCommissions',
            data: { pStartDate: fecIni, pEndDate: fecFin },
            //data: { accountNumber: account },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetCommissionsCxp = function (fecIni, fecFin) {
        return $http({
            method: 'POST',
            url: '/Commissions/GetCommissionsCxp',
            data: { pStartDate: fecIni, pEndDate: fecFin },
            //data: { accountNumber: account },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetBalancesCommissions = function (account, hijo, tipo) {
        return $http({
            method: 'POST',
            url: '/Commissions/GetBalancesCommissions',
            data: {
                accountNumber: account,
                child: hijo,
                type: tipo
            },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDetailCommissionsCxp = function (id) {
        return $http({
            method: 'POST',
            url: '/Commissions/GetDetailCommissionsCxp',
            data: { nro_cxp: id },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.GetCommissionsDescuentos = function (id) {
        return $http({
            method: 'POST',
            url: '/Commissions/GetCommissionsDescuentos',
            data: { nro_cxp: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetBalancesCommissions = function (account) {
        return $http({
            method: 'POST',
            url: '/Commissions/GetBalancesCommissions',
            data: { accountNumber: account },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetHeaderCommisions = function (id, tipo) {
        return $http({
            method: 'POST',
            url: '/Commissions/GetCommissionsHeader',
            data: { childID: id, type: tipo },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetUploadDocuments = function (id) {
        return $http({
            method: 'GET',
            url: '/Commissions/GetUploadDocuments',
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.DownloadFileCommissions = function (file) {
        return $http({
            method: 'POST',
            url: '/Commissions/DownloadFileCommissions',
            data: { FileName: file },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    // PQR
    fac.GetHeaderPQR = function (tipo, fecIni, fecFin, nroCredito, nroPQR, tipoFlujo, estado, hijos) {
        return $http({
            method: 'POST',
            url: '/PQR/GetHeaderPQR',
            data: {
                type: tipo,
                pStartDate: fecIni,
                pEndDate: fecFin,
                loanNumber: nroCredito,
                PQRnumber: nroPQR,
                flowType: tipoFlujo,
                status: estado,
                childs: hijos
            },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetLogPQR = function (id) {
        return $http({
            method: 'POST',
            url: '/PQR/GetLogPQR',
            data: { processNumber: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetNoveltyPQR = function (id) {
        return $http({
            method: 'POST',
            url: '/PQR/GetNoveltyPQR',
            data: { processNumber: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetFlow = function () {
        return $http.get('/PQR/GetFlow');
    };
    fac.GetJustification = function (id) {
        return $http({
            method: 'POST',
            url: '/PQR/GetJustification',
            data: { flowType: id },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.CreatePQR = function (data) {
        return $http({
            method: 'POST',
            url: '/PQR/CreatePQR',
            data: { input: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetLoanResume = function (loan) {
        return $http({
            method: 'POST',
            url: '/PQR/GetLoanResume',
            data: { loanNumber: loan },
            headers: { 'Content-Type': 'application/json' }
        });

    };
    fac.GetStates = function () {
        return $http.get('/PQR/GetStates');
    };
    fac.SimulateCategory = function (data) {
        return $http({
            method: 'POST',
            url: '/Simulator/GetCategorySimulation',
            data: { amount: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetSummaryPQR = function (hijo, tipo) {
        return $http({
            method: 'POST',
            url: '/PQR/GetSummaryPQR',
            data: { child: hijo, type: tipo },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetExecutiveLevel = function () {
        return $http.get('/PQR/GetExecutiveLevel');
    };
    fac.GetExecutiveChilds = function (data) {
        return $http({
            method: 'POST',
            url: '/PQR/GetExecutiveChilds',
            data: { level: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetFolioDetail = function (tipo, fecIni, fecFin, nroFolio, hijos) {
        return $http({
            method: 'POST',
            url: '/Folio/GetFolioDetail',
            data: {
                type: tipo,
                pStartDate: fecIni,
                pEndDate: fecFin,
                folioNumber: nroFolio,
                child: hijos
            },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetNextCategory = function () {
        return $http.get('/ComplianceGoal/GetNextCategory');
    };
    fac.GetAccumulatedLoan = function () {
        return $http.get('/ComplianceGoal/GetAccumulatedLoan');
    };
    fac.GetAccumulatedClarifications = function () {
        return $http.get('/ComplianceGoal/GetAccumulatedClarifications');
    };
    fac.GetGoalExecutive = function (data) {
        return $http.get('/ComplianceGoal/GetGoalExecutive');
    };
    fac.GetProductivity = function (data) {
        return $http.get('/ComplianceGoal/GetProductivity');
    };
    fac.GetCategoryRange = function (data) {
        return $http.get('/ComplianceGoal/GetCategoryRange');
    };
    fac.GetGoalSupervisor = function (data) {
        return $http.get('/ComplianceGoal/GetGoalSupervisor');
    };
    fac.GetProgress = function (fecIni, fecFin, child) {
        return $http({
            method: 'POST',
            url: '/ComplianceGoal/GetProgress',
            data: {
                pStartDate: fecIni,
                pEndDate: fecFin,
                pChild: child
            },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    //Documentos
    fac.GetDocumentsUpload = function (a_tipodoc, a_fechaini, a_fechafin) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocumentsUpload',
            data: { tipodoc: a_tipodoc, fechaini: a_fechaini, fechafin: a_fechafin },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.SubirDocumento = function (data) {
        return $http({
            method: 'POST',
            url: '/Documents/SubirDocumento',
            data: { documents: data },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDocumentsTipDoc = function () {
        return $http.get('/Documents/GetDocumentsTipDoc');
    };
    fac.GetDocumentsTipDocSearch = function () {
        return $http.get('/Documents/GetDocumentsTipDocSearch');
    };
    fac.GetDocumentsLog = function (document) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocumentsLog',
            data: { documento: document },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDocCountxTipDocxState = function (a_tipodoc, a_state) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocCountxTipDocxState',
            data: { tipodoc: a_tipodoc, state: a_state },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDocumentsxTipoDocxState = function (a_tipodoc, a_state) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocumentsxTipoDocxState',
            data: { tipodoc: a_tipodoc, state: a_state },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDocumentsxTipoDocxStateFec = function (a_tipodoc, a_state, a_fechaini, a_fechafin) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocumentsxTipoDocxStateFec',
            data: { tipodoc: a_tipodoc, state: a_state, fechaini: a_fechaini, fechafin: a_fechafin },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDocumentsallxstate = function (a_state) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocumentsallxstate',
            data: { state: a_state },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDocumentsxAsesorxState = function (a_asesor, a_state) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocumentsxAsesorxState',
            data: { asesor: a_asesor, state: a_state },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.GetDocumentsxCedulaxState = function (a_cedula, a_state) {
        return $http({
            method: 'POST',
            url: '/Documents/GetDocumentsxCedulaxState',
            data: { cedula: a_cedula, state: a_state },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.DownloadDocFiles = function (file) {
        return $http({
            method: 'POST',
            url: '/Documents/DownloadDocFiles',
            data: { FileName: file },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.LogUpdateDocuments = function (doc, estado, obs, mensaje) {
        return $http({
            method: 'POST',
            url: '/Documents/LogUpdateDocuments',
            data: { documento: doc, tipo_estado: estado, observacion: obs, detalle: mensaje },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    //Parametros
    /* Avisos */
    fac.getAvisos = function () {
        return $http.get('/Parametros/getAvisos');
    }
    fac.getAvisoActual = function () {
        return $http.get('/Parametros/GetAvisoActual');
    }
    fac.getIdAviso = function (cod) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdAviso',
            data: { cod: cod },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.saveAviso = function (data) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveAviso',
            data: data,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
        });
    }
    fac.updAviso = function (data) {
        return $http({
            method: 'POST',
            url: '/Parametros/updAviso',
            data: data,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
        });
    }
    fac.deleteAviso = function (cod) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteAviso',
            data: { cod: cod },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    /*
     * Dependencias
     */
    fac.getDependencias = function () {
        return $http.get('/Parametros/getDependencias');
    }
    fac.getDependenciasActivas = function () {
        return $http.get('/Parametros/getDependenciasActivas');
    }
    fac.saveDependencia = function (codigo, nombre, estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveDependencia',
            data: { codigo_dep: codigo, dependencia: nombre, estado: estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updDependencia = function (secuencia, nombre, estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/updDependencia',
            data: { secuencia: secuencia, dependencia: nombre, estado: estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteDependencia = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteDependencia',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.loadDependencia = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/loadDependencia',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    // PERIDOOS APLICABLES
    fac.getPeriodos = function () {
        return $http.get('/Parametros/getPeriodos');
    }
    fac.getIdPeriodos = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdPeriodos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.savePeriodos = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/savePeriodos',
            data: { Periodo: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updPeriodos = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updPeriodos',
            data: { secuencia: secuencia, Periodo: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deletePeriodos = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deletePeriodos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    // Plazos
    fac.getPlazosPeriodo = function (periodo) {
        return $http({
            method: 'POST',
            url: '/Parametros/getPlazosPeriodo',
            data: { 'periodoAplicable': periodo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getPlazos = function () {
        return $http.get('/Parametros/getPlazos');
    }
    fac.getIdPlazo = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdPlazo',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.savePlazo = function (nombre, periodo) {
        return $http({
            method: 'POST',
            url: '/Parametros/savePlazos',
            data: { plazo: nombre, periodo: periodo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updPlazo = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updPlazos',
            data: { secuencia: secuencia, plazo: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deletePlazo = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deletePlazos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    // Quincena Descuento
    fac.getDescuentos = function () {
        return $http.get('/Parametros/getQuincenaDescuentos');
    }
    fac.getIdDescuento = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdQuincenaDescuentos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveDescuento = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveQuincenaDescuentos',
            data: { descuetno: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updDescuento = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/upQuincenaDescuentos',
            data: { secuencia: secuencia, descuento: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteDescuento = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteQuincenaDescuentos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Destino Credito
    fac.getDestino = function () {
        return $http.get('/Parametros/getDestinoCredito');
    }
    fac.getIdDestinoCredito = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdDestinoCredito',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveDestinoCredito = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveDestinoCredito',
            data: { destinoCredito: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updDestinoCredito = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updDestinoCredito',
            data: { secuencia: secuencia, destinoCredito: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteDestinoCredito = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteDestinoCredito',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Empresa Telefónica
    fac.getEmpresaTelefonica = function () {
        return $http.get('/Parametros/getEmpresaTelefonica');
    }
    fac.getIdEmpresaTelefonica = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdEmpresaTelefonica',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveEmpresaTelefonica = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveEmpresaTelefonica',
            data: { empresaTelefonica: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updEmpresaTelefonica = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updEmpresaTelefonica',
            data: { secuencia: secuencia, empresaTelefonica: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteEmpresaTelefonica = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteEmpresaTelefonica',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Estado Civil
    fac.getEstadoCivil = function () {
        return $http.get('/Parametros/getEstadoCivil');
    }
    fac.getIdEstadoCivil = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdEstadoCivil',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updEstadoCivil = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updEstadoCivil',
            data: { secuencia: secuencia, estadoCivil: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveEstadoCivil = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveEstadoCivil',
            data: { estadoCivil: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updEstadoCivil = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updEstadoCivil',
            data: { secuencia: secuencia, estadoCivil: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteEstadoCivil = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteEstadoCivil',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Identificacion
    fac.getTiposIdentificacion = function () {
        return $http.get('/Parametros/getTiposIdentificacion');
    }
    fac.getIdTiposIdentificacion = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdTiposIdentificacion',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveTiposIdentificacion = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveTiposIdentificacion',
            data: { tipoIdentificacion: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updTiposIdentificacion = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updTiposIdentificacion',
            data: { secuencia: secuencia, tipoIdentificacion: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteTiposIdentificacion = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteTiposIdentificacion',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Ingresos
    fac.getIngresos = function () {
        return $http.get('/Parametros/getIngresos');
    }
    fac.getIdIngresos = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdIngresos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveIngresos = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveIngresos',
            data: { Ingresos: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updIngresos = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updIngresos',
            data: { secuencia: secuencia, Ingresos: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteIngresos = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteIngresos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Medios
    fac.getMediosDisposicion = function () {
        return $http.get('/Parametros/getMediosDisposicion');
    }
    fac.getIdMediosDisposicion = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdMediosDisposicion',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveMediosDisposicion = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveMediosDisposicion',
            data: { Medios: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updMediosDisposicion = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updMediosDisposicion',
            data: { secuencia: secuencia, Medios: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteMediosDisposicion = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteMediosDisposicion',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Nomina
    fac.getTipoNomina = function () {
        return $http.get('/Parametros/getTipoNomina');
    }
    fac.getIdTipoNomina = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdTipoNomina',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveTipoNomina = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveTipoNomina',
            data: { tipoNomina: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updTipoNomina = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updTipoNomina',
            data: { secuencia: secuencia, tipoNomina: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteTipoNomina = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteTipoNomina',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Dias Expiracion
    fac.getDiasExp = function () {
        return $http.get('/Parametros/getDiasExp');
    }
    fac.getIdDiasExp = function (diasexp) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdDiasExp',
            data: { dias: diasexp },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveDiasExp = function (diasexp) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveDiasExp',
            data: { dias: diasexp },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Sector
    fac.getSector = function () {
        return $http.get('/Parametros/getSector');
    }
    fac.getIdSector = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdSector',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveSector = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveSector',
            data: { sector: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updSector = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updSector',
            data: { secuencia: secuencia, sector: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteSector = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteSector',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //SectorGuias
    fac.getSectorGuias = function () {
        return $http.get('/Parametros/getSectorGuias');
    }
    fac.getIdSectorGuias = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdSectorGuias',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveSectorGuias = function (nombre, ind_estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveSectorGuias',
            data: { sector: nombre, estado: ind_estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updSectorGuias = function (secuencia, nombre, ind_estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/updSectorGuias',
            data: { secuencia: secuencia, sector: nombre, estado: ind_estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteSectorGuias = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteSectorGuias',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //SectorTablas
    fac.getSectorTablas = function () {
        return $http.get('/Parametros/getSectorTablas');
    }
    fac.getIdSectorTablas = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdSectorTablas',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveSectorTablas = function (nombre, ind_estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveSectorTablas',
            data: { sector: nombre, estado: ind_estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updSectorTablas = function (secuencia, nombre, ind_estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/updSectorTablas',
            data: { secuencia: secuencia, sector: nombre, estado: ind_estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteSectorTablas = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteSectorTablas',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Puestos
    fac.getPuestos = function () {
        return $http.get('/Parametros/getPuestos');
    }
    fac.getIdPuestos = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdPuestos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.savePuestos = function (sector, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/savePuestos',
            data: { sector: sector, puestos: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updPuestos = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updPuestos',
            data: { secuencia: secuencia, puestos: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deletePuestos = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deletePuestos',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getPuestoSector = function (sector) {
        return $http({
            method: 'POST',
            url: '/Parametros/getPuestoSector',
            data: { sector: sector },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Reca
    fac.getReca = function () {
        return $http.get('/Parametros/getReca');
    }
    fac.getIdReca = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdReca',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveReca = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveReca',
            data: { Reca: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updReca = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updReca',
            data: { secuencia: secuencia, Reca: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteReca = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteReca',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Sucursales
    fac.getSucursales = function () {
        return $http.get('/Parametros/getSucursales');
    }
    fac.getIdSucursales = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdSucursales',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveSucursales = function (nombre, identificador, tipo, division, region, emailCoordinador, emailAux) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveSucursales',
            data: {
                sucursal: nombre, id_sucursal: identificador, tipo: tipo, division: division, region: region,
                emailCoordinador: emailCoordinador, emailAuxiliar: emailAux
            },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updSucursales = function (secuencia, nombre, tipo, emailCoordinador, emailAux) {
        return $http({
            method: 'POST',
            url: '/Parametros/updSucursales',
            data: {
                secuencia: secuencia, sucursal: nombre, tipo_sucursal: tipo,
                emailCoordinador: emailCoordinador, emailAuxiliar: emailAux
            },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteSucursales = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteSucursales',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Tipo de Solicitud
    fac.getTipoSolicitud = function () {
        return $http.get('/Parametros/getTipoSolicitud');
    }
    fac.getIdTipoSolicitud = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdTipoSolicitud',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveTipoSolicitud = function (nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveTipoSolicitud',
            data: { tipoSolicitud: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updTipoSolicitud = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updTipoSolicitud',
            data: { secuencia: secuencia, tipoSolicitud: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteTipoSolicitud = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteTipoSolicitud',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Puestos
    fac.getProductos = function () {
        return $http.get('/Parametros/getProductos');
    }
    fac.getIdProducto = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdProducto',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveProductos = function (codigo, dependencia, nombre, estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveProductos',
            data: { codigo_pro: codigo, dependencia: dependencia, producto: nombre, estado: estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updProducto = function (secuencia, nombre, estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/updProducto',
            data: { secuencia: secuencia, producto: nombre, estado: estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteProducto = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteProducto',
            data: { secuencia: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getProductosDependencia = function (dependencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getProductosDependencia',
            data: { dependencia: dependencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.savePlazoCSV = function (periodo, plazo) {
        return $http({
            method: 'POST',
            url: '/Parametros/savePlazoCSV',
            data: { periodo: periodo, plazo: plazo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.savePuestoCSV = function (sector, puesto) {
        return $http({
            method: 'POST',
            url: '/Parametros/savePuestoCSV',
            data: { sector: sector, puesto: puesto },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveProductoCSV = function (dependencia, producto) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveProductoCSV',
            data: { dependencia: dependencia, producto: producto },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getDivision = function () {
        return $http.get('/Parametros/getDivision');
    }
    fac.getRegionDivision = function (division) {
        return $http({
            method: 'POST',
            url: '/Parametros/getRegionDivision',
            data: { division: division },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getTipoSucursal = function () {
        return $http.get('/Parametros/getTipoSucursal');
    }
    fac.savePais = function (codigo, nombre, codigo_pais) {
        return $http({
            method: 'POST',
            url: '/Parametros/savePais',
            data: { codigo: codigo, nombre: nombre, codigo_pais: codigo_pais },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updPais = function (codigo, nombre, codigo_pais) {
        return $http({
            method: 'POST',
            url: '/Parametros/updPais',
            data: { id_pais: codigo, nombre_pais: nombre, codigo_pais: codigo_pais },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deletePais = function (codigo) {
        return $http({
            method: 'POST',
            url: '/Parametros/deletePais',
            data: { id_pais: codigo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getPais = function () {
        return $http.get('/Parametros/getPais');
    }
    fac.getIdPais = function (codigo) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdPais',
            data: { id_pais: codigo },
            headers: { 'Content-Type': 'application/json' }
        });
    }

    fac.saveEntidad = function (codigo, nombre, codigo_pais, abrev) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveEntidad',
            data: { codigo: codigo, nombre: nombre, codigo_pais: codigo_pais, abreviatura: abrev },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getEntidadesFederativas = function () {
        return $http.get('/Parametros/getEntidadFederativa');
    }
    fac.getEntidadPais = function (pais) {
        return $http({
            method: 'POST',
            url: '/Parametros/getEntidadPais',
            data: { pais: pais },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getIdEntidad = function (codigo) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdEntidad',
            data: { codigo_entidad: codigo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updEntidad = function (codigo, nombre, abrev) {
        return $http({
            method: 'POST',
            url: '/Parametros/updEntidad',
            data: { codigo_entidad: codigo, nombre_entidad: nombre, abreviatura: abrev },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteEntidadFederativa = function (codigo) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteEntidad',
            data: { codigo_entidad: codigo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Municipios
    fac.saveMunicipio = function (codigo, nombre, codigo_entidad, pais) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveMunicipio',
            data: { codigo: codigo, nombre: nombre, codigo_entidad: codigo_entidad, cod_pais: pais },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteMunicipio = function (codigo, pais, entidad) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteMunicipio',
            data: { codigo_municipio: codigo, pais: pais, entidad: entidad },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getMunicipios = function () {
        return $http.get('/Parametros/getMunicipios');
    }
    fac.getMunicipioEntidad = function (pais, entidad) {
        return $http({
            method: 'POST',
            url: '/Parametros/getMunicipioEntidad',
            data: { pais: pais, entidad: entidad },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getIdMunicipio = function (codigo, pais, entidad) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdMunicipios',
            data: {
                codigo_municipio: codigo, pais: pais, entidad: entidad
            },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updMunicipio = function (codigo, nombre_municipio, pais, entidad) {
        return $http({
            method: 'POST',
            url: '/Parametros/updMunicipio',
            data: { codigo_municipio: codigo, municipio: nombre_municipio, pais: pais, entidad: entidad },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Sucursal Asesor
    fac.getSucursalAsesor = function () {
        return $http.get('/Profile/getSucursalAsesor')
    }
    //Campos Adicionales
    fac.saveCampos = function (tipoSolicitud, dependencia, producto, periodo, campo, tipo, opciones, requerido) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveCampos',
            data: {
                tipoSolicitud: tipoSolicitud, dependencia: dependencia, producto: producto, periodo: periodo,
                campo: campo, tipo: tipo, opciones: opciones, requerido: requerido
            },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getCampos = function () {
        return $http.get('/Parametros/getCampos');
    }
    fac.getIdCampo = function (cod_campo) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdCampos',
            data: { cod_campo: cod_campo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updCampo = function (cod_campo, campo, tipo, opciones, requerido) {
        return $http({
            method: 'POST',
            url: '/Parametros/updCampos',
            data: { cod_campo: cod_campo, campo: campo, tipo: tipo, opciones: opciones, requerido: requerido },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteCampo = function (cod_campo) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteCampos',
            data: { cod_campo: cod_campo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveCamposCSV = function (tipoSolicitud, dependencia, producto, periodo, campo, tipo, opciones, requerido) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveCamposCSV',
            data: {
                tipoSolicitud: tipoSolicitud, dependencia: dependencia, producto: producto, perido: periodo,
                campo: campo, dato: tipo, opciones: opciones, obligatorio: requerido
            },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.campoAdicional = function (tipoSolicitud, dependencia, producto, periodo) {
        return $http({
            method: 'POST',
            url: '/Profile/campoAdicional',
            data: { tipoSolicitud: tipoSolicitud, dependencia: dependencia, producto: producto, periodo: periodo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    /* Documentos */
    fac.getDocumentos = function (dependencia, producto) {
        return $http({
            method: 'POST',
            url: '/Parametros/getDocumentos',
            data: { dependencia: dependencia, producto: producto },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getDocumentosConfig = function (dependencia, producto) {
        return $http({
            method: 'POST',
            url: '/Parametros/getDocumentosConfig',
            data: { dependencia: dependencia, producto: producto },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getDocumentosOriginacion = function (dependencia, producto) {
        return $http({
            method: 'POST',
            url: '/Parametros/getDocumentosOriginacion',
            data: { dependencia: dependencia, producto: producto },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveDocumentos = function (data) {
        return $http({
            /*method: 'POST',
            url: '/Parametros/saveDocumentos',
            data: { documento: JSON.stringify(data) },
            headers: { 'Content-Type': 'application/json' }*/
            method: 'POST',
            url: '/Parametros/saveDocumentos',
            headers: { "Content-Type": undefined },
            data: data,
            transformRequest: angular.identity
        });
    }
    fac.updDocumentos = function (data) {
        return $http({
            /*method: 'POST',
            url: '/Parametros/updDocumentos',
            data: { documento: JSON.stringify(data) },
            headers: { 'Content-Type': 'application/json' }*/
            method: 'POST',
            url: '/Parametros/updDocumentos',
            headers: { "Content-Type": undefined },
            data: data,
            transformRequest: angular.identity
        });
    }
    fac.getIdDocumento = function (codigo) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdDocumentos',
            data: { cod_documento: codigo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteDocumentos = function (codigo) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteDocumentos',
            data: { cod_documento: codigo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Configuración de Documentos.
    fac.saveConfigDoc = function (cod_doc, t_ootencion, posc_x, posc_y, valor, pagina, fuente, valida, campo_compara, valor1, aumenta) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveConfigDoc',
            data: {
                documento: cod_doc, obtencion: t_ootencion, x: posc_x, y: posc_y, dato: valor, pagina: pagina, fuente: fuente,
                valida: valida, campoComparar: campo_compara, valorComparar: valor1, variacion: aumenta
            },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updConfigDoc = function (codigo_config, cod_doc, t_ootencion, posc_x, posc_y, valor, pagina, fuente, valida, campo_compara, valor1, aumenta) {
        return $http({
            method: 'POST',
            url: '/Parametros/updConfigDoc',
            data: {
                codigo: codigo_config, documento: cod_doc, obtencion: t_ootencion, x: posc_x, y: posc_y, dato: valor, pagina: pagina, fuente: fuente,
                valida: valida, campoComparar: campo_compara, valorComparar: valor1, variacion: aumenta
            },
            headers: { 'Content-Type': 'application/json' }
        });

    }
    fac.deleteConfigDoc = function (codigo_config) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteConfigDoc',
            data: { codigo: codigo_config },
            headers: { 'Content-Type': 'application/json' }
        });

    }
    fac.getConfigDocumentos = function (dependencia, producto, documento) {
        return $http({
            method: 'POST',
            url: '/Parametros/getConfigDocumentos',
            data: { dependencia: dependencia, producto: producto, doc: documento },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getIdConfigDocumentos = function (codigo_config) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdConfigDocumentos',
            data: { codigo: codigo_config },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.camposFormulario = function () {
        return $http.get('/Parametros/camposFormulario');
    }
    //Bancos 
    fac.saveBanco = function (cod_ban, nom_banco) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveBanco',
            data: { codigo: cod_ban, banco: nom_banco },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updBanco = function (cod_ban, nom_banco) {
        return $http({
            method: 'POST',
            url: '/Parametros/updBanco',
            data: { codigo: cod_ban, banco: nom_banco },
            headers: { 'Content-Type': 'application/json' }
        });

    }
    fac.deleteBanco = function (cod_ban) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteBanco',
            data: { codigo: cod_ban },
            headers: { 'Content-Type': 'application/json' }
        });

    }
    fac.getBanco = function () {
        return $http.get('/Parametros/getBanco');
    }
    fac.getIdBanco = function (cod_ban) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdBanco',
            data: { codigo: cod_ban },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Casas Financieras
    fac.getCasaFinanciera = function () {
        return $http.get('/Parametros/getCasas');
    }
    fac.getCasasFinancierasActivas = function () {
        return $http.get('/Parametros/getCasasActivas');
    }
    fac.saveCasaFinanciera = function (codigo, nombre, estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveCasa',
            data: { rfc: codigo, casa_financiera: nombre, estado: estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updCasaFinanciera = function (secuencia, nombre, estado) {
        return $http({
            method: 'POST',
            url: '/Parametros/updCasa',
            data: { rfc: secuencia, casa_financiera: nombre, estado: estado },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteCasasFinanciera = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteCasa',
            data: { rfc: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getIdCasaFinanciera = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdCasas',
            data: { rfc: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Casas Financieras
    fac.getClaves = function () {
        return $http.get('/Parametros/getClaves');
    }
    fac.saveClaveDelg = function (codigo, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/saveClaves',
            data: { cod: codigo, delg: nombre },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.updClaveDelg = function (secuencia, nombre) {
        return $http({
            method: 'POST',
            url: '/Parametros/updClaves',
            data: { cod: secuencia, delg: nombre,  },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.deleteClave = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/deleteClaves',
            data: { cod: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.getIdClave = function (secuencia) {
        return $http({
            method: 'POST',
            url: '/Parametros/getIdClaves',
            data: { cod: secuencia },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    //Originacion
    fac.writePDF = function (dependencia, producto) {
        return $http({
            method: 'POST',
            url: '/Profile/writePDF',
            data: { dependencia: dependencia, producto: producto },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.buscarCliente = function (rfc) {
        return $http({
            method: 'POST',
            url: '/Profile/buscarCliente',
            data: { rfc: rfc },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.saveFormulario = function (tab, formulario) {
        return $http({
            method: 'POST',
            url: '/Profile/saveFormulario',
            data: { pestana: tab, formulario: JSON.stringify(formulario) },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.docConfig = function (dependencia, producto, documento) {
        return $http({
            method: 'POST',
            url: '/Parametros/pruebaConfiguracion',
            data: { dependencia: dependencia, producto: producto, doc: documento },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.guardaDocumentoOriginacion = function (data) {
        return $http({
            method: 'POST',
            url: '/Profile/guardaDocumentoOriginacion',
            data: data,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
        });
    }
    fac.guardaDocumentoOriginacionCompra = function (data) {
        return $http({
            method: 'POST',
            url: '/Profile/guardaDocumentoOriginacionCompra',
            data: data,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
        });
    }
    fac.guardaDocumentoOriginacion1 = function (file) {
        return $http({
            method: 'POST',
            url: '/Profile/guardaDocumentoOriginacion1',
            headers: { "Content-Type": undefined },
            data: file,
            transformRequest: angular.identity
        });
    }
    fac.buscarDocumentos = function (doc, folder) {
        return $http({
            method: 'POST',
            url: '/Profile/buscarDocumentos',
            data: { folder: folder, codigoDoc: doc },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.eliminaDocumentosOriginacion = function (doc) {
        return $http({
            method: 'POST',
            url: '/Profile/eliminaDocumentosOriginacion',
            data: { codigoDoc: doc },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.documentoOriginacion = function (dependencia, producto, folder, documento) {
        return $http({
            method: 'POST',
            url: '/Profile/documentoOriginacion',
            data: { dependencia: dependencia, producto: producto, doc: documento, folder: folder },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.documentoCartera = function (dependencia, producto, folder, documento) {
        return $http({
            method: 'POST',
            url: '/Profile/documentoCartera',
            data: { dependencia: dependencia, producto: producto, doc: documento, folder: folder },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.listSolicitudes = function () {
        return $http.get('/Profile/listSolicitudes');
    }
    fac.DocSoloFirmas = function (dependencia, producto, folder) {
        return $http({
            method: 'POST',
            url: '/Profile/soloFirmas',
            data: { folder: folder, dependencia: dependencia, producto: producto },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.Expedientillo = function (dependencia, producto, folder, cambios) {
        return $http({
            method: 'POST',
            url: '/Profile/Expedientillo',
            data: { folder: folder, dependencia: dependencia, producto: producto, cambioDoc: cambios},
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.allDocs = function (dependencia, producto, folder, cambios) {
        return $http({
            method: 'POST',
            url: '/Profile/AllDocuments',
            data: { folder: folder, dependencia: dependencia, producto: producto, cambioDoc : cambios},
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.impresion = function (dependencia, producto, folder, cambios) {
        return $http({
            method: 'POST',
            url: '/Profile/impresion',
            data: { folder: folder, dependencia: dependencia, producto: producto, cambioDoc: cambios },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.getIdSolicitud = function (folder) {
        return $http({
            method: 'POST',
            url: '/Profile/getIdSolicitud',
            data: { folder: folder },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.eliminarItemCartera = function (item, folder) {
        return $http({
            method: 'POST',
            url: '/Profile/eliminarItemCartera',
            data: { folder: folder, item: item },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.getOfertasSeguros = function (folder, vlrSolicitado, vlrMaximo) {
        return $http({
            method: 'POST',
            url: '/Profile/getOfertasSeguros',
            data: { folder: folder, vlr_solcitado: vlrSolicitado, vlr_maximo: vlrMaximo },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.aceptaOferta = function (data) {
        return $http({
            method: 'POST',
            url: '/Profile/aceptarOferta',
            data: { formulario: JSON.stringify(data) },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    //Gias y Tablas
    fac.GetddSectorTablas = function () {
        return $http.get('/Documents/GetddSectorTablas');
    }
    fac.GetddSectorGuias = function () {
        return $http.get('/Documents/GetddSectorGuias');
    }
    //Asesor
    fac.GetGuiasAs = function () {
        return $http.get('/Documents/GetGuiasAs');
    }
    fac.getTablasAs = function () {
        return $http.get('/Documents/GetTablasAs');
    }
    //Coordinador
    fac.GetGuias = function () {
        return $http.get('/Documents/GetGuias');
    }
    fac.GetTablas = function () {
        return $http.get('/Documents/GetTablas');
    }
    fac.GetIdGuias = function (data) {
        return $http({
            method: 'POST',
            url: '/Documents/GetIdGuias',
            data: { codigo: data },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.GetIdTablas = function (data) {
        return $http({
            method: 'POST',
            url: '/Documents/GetIdTablas',
            data: { codigo: data },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.GuardarArchivo = function (data, opcion) {
        return $http({
            method: 'POST',
            url: '/Documents/GuardarArchvio',
            data: { archivo: JSON.stringify(data), opcion: opcion },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.EditarArchivo = function (data, opcion) {
        return $http({
            method: 'POST',
            url: '/Documents/EditarArchvio',
            data: { archivo: JSON.stringify(data), opcion: opcion },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.eliminarArchvio = function (data, opcion) {
        return $http({
            method: 'POST',
            url: '/Documents/EliminarArchvio',
            data: { codigo: data, opcion: opcion },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.calcularOferta = function (data) {
        return $http({
            method: 'POST',
            url: '/Profile/calcularOferta',
            data: { formulario: JSON.stringify(data) },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.procesarSolicitud = function (folder, dependencia, producto) {
        return $http({
            method: 'POST',
            url: '/Profile/procesarOferta',
            data: { folder: folder, dependencia, dependencia, producto, producto },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.guardarBeneficiario = function (obj, id_poliza) {
        return $http({
            method: 'POST',
            url: '/Profile/guardarBeneficiarioPoliza',
            data: { beneficiario: JSON.stringify(obj), poliza: id_poliza },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.beneficiariosPoliza = function (folder) {
        return $http({
            method: 'POST',
            url: '/Profile/beneficiariosPoliza',
            data: { folder: folder },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    fac.Listparentesco = function () {
        return $http.get('/Parametros/parentesco');
    }
    fac.entidadAbrev = function (abrv) {
        return $http({
            method: 'POST',
            url: '/Profile/entidadAbreviatura',
            data: { abreviatura: abrv },
            headers: { 'Content-Type': 'application/json' },
        });
    }
    /*Danny 2019 10*/
    fac.getComentarios = function (carpeta) {
        return $http({
            method: 'POST',
            url: '/Profile/getComentarios',
            data: { folder: carpeta },
            headers: { 'Content-Type': 'application/json' }
        });
    };

    fac.getDashBoard = function () {
        return $http.get('/Profile/getDashBoard');
    };
    /*Danny 2019 10*/

    fac.docPoliza = function (carpeta) {
        return $http({
            method: 'POST',
            url: '/Profile/docPoliza',
            data: { folder: carpeta },
            headers: { 'Content-Type': 'application/json' }
        });
    };
    fac.codigoPostal = function (codigo) {
        return $http({
            method: 'POST',
            url: '/Profile/BuscaCodigoPostal',
            data: { codigoPostal: codigo },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.findEntidad = function (entidad) {
        return $http({
            method: 'POST',
            url: '/Profile/findEntidad',
            data: { entidadFederativa: entidad },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.findMunicipio = function (municipio) {
        return $http({
            method: 'POST',
            url: '/Profile/findMunicipio',
            data: { Municipio: municipio },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.cancelarFolio = function (folder) {
        return $http({
            method: 'POST',
            url: '/Profile/cancelarFolio',
            data: { folder: folder },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    fac.findExpCompleto = function (folder) {
        return $http({
            method: 'POST',
            url: '/Profile/findExpCompleto',
            data: { folder: folder },
            headers: { 'Content-Type': 'application/json' }
        });
    }
    return fac;
});
