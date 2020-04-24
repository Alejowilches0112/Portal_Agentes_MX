var app = angular.module('appBayport');
app.controller('DocumentosController', function ($scope, BayportService, $filter, $location, $window) {
    $scope.objDelete = function (item) {
        $scope.objEliminar = item;
    }
    $scope.openModal = function (modal) {
        console.log('abrir');
        $('#' + modal).modal('show');
    }
    $scope.closeModal = function (modal) {
        console.log('cerrar')
        $('#' + modal).modal('toggle');
    }

    $scope.uploadedFileGuia = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            console.log($scope.file);
            var boton = element.id;
            //var prueba = "#" + boton;
            $scope.nombreArchivo = element.files["0"].name;
            var nombreLabel = "#" + boton + 'Doc';
            console.log(boton)
            $scope.nameLab = nombreLabel;
            $(nombreLabel).text($scope.nombreArchivo);
            var reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.file64 = e.target.result.split("base64,")[1];
                });
            }
            reader.readAsDataURL($scope.file);
            var value = element.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                //$scope.file64 = e.target.result.split("base64,")[1];
                $scope.guia.file = e.target.result.split("base64,")[1];
                var fileCom = atob($scope.guia.file);
                switch ($scope.file.type) {
                    case 'application/pdf':
                        if (fileCom.indexOf('%PDF-1.') === -1) {
                            $scope.guia.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(botom).value = '';
                            return;
                        }
                        break;
                    case 'image/png':
                        if (fileCom.indexOf('PNG') === -1) {
                            $scope.guia.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(botom).value = '';
                            return;
                        }
                        break;
                    case 'image/jpeg':
                        if (fileCom.indexOf('JFIF') > -1 && ($scope.file.name.indexOf('.jpg') === -1 && $scope.file.name.indexOf('.jpeg') === -1)) {
                            $scope.guia.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(botom).value = '';
                            return;
                        }
                        break;
                    default:
                        $scope.guia.file = null;
                        alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                        $(nombreLabel).text('');
                        document.getElementById(botom).value = '';
                        return;
                        break;
                }
                $scope.guia.nombre = $scope.nombreArchivo;
            }
            reader.readAsDataURL(value);
        });
    }

    $scope.uploadedFileTabla = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            
            var boton = element.id;
            //var prueba = "#" + boton;
            $scope.nombreArchivo = element.files["0"].name;
            var nombreLabel = "#" + boton + "Doc";
            $(nombreLabel).text($scope.nombreArchivo);
            var reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.file64 = e.target.result.split("base64,")[1];
                });
            }
            reader.readAsDataURL($scope.file);
            var value = element.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                $scope.tabla.file = e.target.result.split("base64,")[1];
                var fileCom = atob($scope.tabla.file);
                switch ($scope.file.type) {
                    case 'application/pdf':
                        if (fileCom.indexOf('%PDF-1.') === -1) {
                            $scope.tabla.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(botom).value = '';
                            return;
                        }
                        break;
                    case 'image/png':
                        if (fileCom.indexOf('PNG') === -1) {
                            $scope.tabla.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(botom).value = '';
                            return;
                        }
                        break;
                    case 'image/jpeg':
                        if (fileCom.indexOf('JFIF') > -1 && ($scope.file.name.indexOf('.jpg') === -1 && $scope.file.name.indexOf('.jpeg') === -1)) {
                            $scope.tabla.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(botom).value = '';
                            return;
                        }
                        break;
                    default:
                        $scope.tabla.file = null;
                        alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                        $(nombreLabel).text('');
                        document.getElementById(botom).value = '';
                        return;
                        break;
                }
                $scope.tabla.nombre = $scope.nombreArchivo;
            }
            reader.readAsDataURL(value);
        });
    }

    $scope.GetddSectorTablas = function () {
        BayportService.GetddSectorTablas().then(function (d) {
            $scope.ddSectorTablas = d.data;
        });
    }
    $scope.GetddSectorGuias = function () {
        BayportService.GetddSectorGuias().then(function (d) {
            $scope.ddSectorGuias = d.data;
        });
    }
    $scope.SetGu = function (sec) {
        if ($scope.Guias.ListDoc.length > 0) {
            $scope['GuiaXSector' + sec] = $scope.Guias.ListDoc.filter(d => d.sector == sec);
            for (var i = 0; i < $scope['GuiaXSector' + sec].length; i++) {
                var item = $scope['GuiaXSector' + sec][i];
                item.no = i + 1;
            }
        }
        return $scope.GuiaXSector = $scope['GuiaXSector' + sec];
    }

    $scope.getGuias = function () {
        BayportService.GetGuias().then(function (d) {
            $scope.Guias = d.data;
        });
    }
    $scope.getGuiasAs = function () {
        BayportService.GetGuiasAs().then(function (d) {
            $scope.Guias = d.data;
        });
    }

    $scope.SetTa = function (sec) {
        if ($scope.Tablas.ListDoc.length > 0) {
            $scope['TablaXSector' + sec] = $scope.Tablas.ListDoc.filter(d => d.sector == sec);
            for (var i = 0; i < $scope['TablaXSector' + sec].length; i++) {
                var item = $scope['TablaXSector' + sec][i];
                item.no = i + 1;
            }
        }
        return $scope.TablaXSector = $scope['TablaXSector' + sec];
    }

    $scope.getTablasAs = function () {
        BayportService.getTablasAs().then(function (d) {
            $scope.Tablas = d.data;
        });
    }

    $scope.getTablas = function () {
        BayportService.GetTablas().then(function (d) {
            $scope.Tablas = d.data;
        });
    }
    $scope.saveGuia = function () {
        if ($scope.guia.file && $scope.guia.sector) {
            $scope.tiposector = $scope.ddSectorGuias.ListSectorGuias.filter(d => d.secuencia == $scope.guia.sector);
            $scope.guia.nsector = $scope.tiposector[0].sector;
            BayportService.GuardarArchivo($scope.guia, 'GUIA').then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.closeModal('myModalNueva');
                    $scope.guia = {};
                    $("#data-GuiaDoc").text('');
                    $scope.getGuias();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe cargar un documento o escoger un sector');
        }
    }
    $scope.editarGuia = function () {
        if ($scope.guia.sector) {
            $scope.tiposector = $scope.ddSectorGuias.ListSectorGuias.filter(d => d.secuencia == $scope.guia.sector);
            $scope.guia.nsector = $scope.tiposector[0].sector;
            BayportService.EditarArchivo($scope.guia, 'GUIA').then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.closeModal('myModalEdit');
                    $scope.guia = {};
                    $scope.getGuias();
                    $("#data-plantillaDoc").text('');
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe escoger un sector');
        }
    }
    $scope.deleteGuia = function (cod) {
        BayportService.eliminarArchvio(cod, 'GUIA').then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.closeModal('myModalDelete');
                $scope.objEliminar = {};
                $scope.getGuias();
            }
        })
    }

    $scope.saveTabla = function () {
        console.log($scope.tabla)
        if ($scope.tabla.file && $scope.tabla.sector) {
            $scope.tiposector = $scope.ddSectorTablas.ListSectorTablas.filter(d => d.secuencia == $scope.tabla.sector);
            $scope.tabla.nsector = $scope.tiposector[0].sector;
            BayportService.GuardarArchivo($scope.tabla, 'TABLAS').then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.closeModal('myModalNueva');
                    $scope.tabla = {};
                    $scope.getTablas();
                    $("#data-tablaDoc").text('');
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe cargar un documento o escoger un sector');
        }
    }
    $scope.updTabla = function () {
        if ($scope.tabla.sector) {
            $scope.tiposector = $scope.ddSectorTablas.ListSectorTablas.filter(d => d.secuencia == $scope.tabla.sector);
            $scope.tabla.nsector = $scope.tiposector[0].sector;
            BayportService.EditarArchivo($scope.tabla, 'TABLAS').then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.closeModal('myModalEdit');
                    $scope.tabla = {};
                    $scope.getTablas();
                    $("#data-tablaEditDoc").text('');
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe escoger un sector');
        }
    }
    $scope.deleteTabla = function (cod) {
        BayportService.eliminarArchvio(cod, 'TABLAS').then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.closeModal('myModalDelete');
                $scope.objEliminar = {};
                $scope.getTablas();
            }
        })
    }
    $scope.getIdGuia = function (cod) {
        BayportService.GetIdGuias(cod).then(function (d) {
            if (d.data) {
                $scope.guia = d.data;
                $scope.openModal('myModalEdit');
            } else {
                alert('Error Consultando la Guia');
            }
        })
    }
    $scope.getIdTabla = function (cod) {
        BayportService.GetIdTablas(cod).then(function (d) {
            if (d.data) {
                $scope.tabla = d.data;
                $scope.openModal('myModalEdit');
            } else {
                alert('Error Consultando la Guia');
            }
        })
    }
    $scope.DownloadDocFiles = function (url) {
        BayportService.DownloadDocFiles(url).then(function (d) {
            console.log('DownloadDocFiles ' + d.data.virtualpath);
            d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') :  d.data.virtualpath;
            $window.open(d.data.virtualpath, '_blank', '');
        }, function (error) {
            alert('Error GetDocumentsxAsesorxState!');
        });
    };
});

app.controller('ConsultarController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";
    $scope.visible = false;
    $scope.cabeceras = false;

    $scope.dates = {};
    $scope.years = [];
    var current_year = new Date().getFullYear();
    for (var i = current_year - 7; i <= current_year; i++) {
        $scope.years.push({ value: i + '' });
    }



    $scope.arr = ['Seleccione Mes', 'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
    $scope.selec = 0;




    $scope.ConsultarSolicitudes = function () {
        $scope.Solicitudes = {};
        $scope.EncabezadoCredito = {};
        $scope.visible = false;
        $scope.cabeceras = false;
        BayportService.GetRequisitionByClient($scope.Document).then(function (d) {
            $scope.Solicitudes = d.data;
            console.log($scope.Solicitudes);
            $scope.visible = true;

        }, function (error) {
            alert('Error ConsultarSolicitudes!');
        });
    };


    $scope.ConsultarSolicitudesByDate = function () {
        $scope.Solicitudes = {};
        $scope.EncabezadoCredito = {};
        $scope.visible = false;
        $scope.cabeceras = false;

        if ($scope.request != undefined) {
            BayportService.GetRequisitionByDate($scope.request.fecIni, $scope.request.fecFin).then(function (d) {
                $scope.Solicitudes = d.data;

                console.log($scope.Solicitudes);
                $scope.visible = true;

            }, function (error) {
                alert('Error ConsultarSolicitudes!');
            });
        }

    };

    $scope.ConsultarCredito = function (index) {
        BayportService.GetLoanHeader($scope.Solicitudes.loanInformationList[index].folderNumber).then(function (d) {
            $scope.EncabezadoCredito = d.data;
            console.log($scope.EncabezadoCredito);

        }, function (error) {
            alert('Error ConsultarEncabezadoCredito!');
        });

        BayportService.GetLoanDetailList($scope.Solicitudes.loanInformationList[index].folderNumber).then(function (d) {
            $scope.DetalleCredito = d.data;
            console.log($scope.DetalleCredito);
            $scope.cabeceras = true;
        }, function (error) {
            alert('Error GetLoanDetailList!');
        });
    };

});

app.controller('ActualizarController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";

    BayportService.getBanco().then(function (d) {
        $scope.listBancos = d.data;
        console.log($scope.listBancos)
    }, function (err) {
        alert(err);
    });

    $scope.LoadList = function () {
        BayportService.GetCivilStatus().then(function (d) {
            $scope.EstadoCivilList = d.data;
        }, function (error) {
            alert('Error GetEstadoCivil!');
        });

        BayportService.GetDepartments().then(function (d) {
            $scope.DepartmentsList = d.data;
        },
            function (error) {
                alert('Error GetDepartments!');
            });

        BayportService.GetHousingType().then(function (d) {
            $scope.HousingTypeList = d.data;
        },
            function (error) {
                alert('Error GetHousingType!');
            });

        BayportService.GetAppliedStudies().then(function (d) {
            $scope.AppliedStudiesList = d.data;
        },
            function (error) {
                alert('Error GetAppliedStudies!');
            });

        BayportService.GetAFP().then(function (d) {
            $scope.AFPList = d.data;
        },
            function (error) {
                alert('Error GetAFP!');
            });

        BayportService.GetARP().then(function (d) {
            $scope.ARPList = d.data;
        },
            function (error) {
                alert('Error GetARP!');
            });

        BayportService.GetEPS().then(function (d) {
            $scope.EPSList = d.data;
        },
            function (error) {
                alert('Error GetEPS!');
            });

        BayportService.GetBanks().then(function (d) {
            $scope.BankList = d.data;
        },
            function (error) {
                alert('Error GetBanks!');
            });

        BayportService.GetBornCity().then(function (d) {
            $scope.BornCityList = d.data;
        },
            function (error) {
                alert('Error GetBornCity!');
            });

        BayportService.GetCancellationCausal().then(function (d) {
            $scope.CausalCancelationList = d.data;
        },
            function (error) {
                alert('Error GetCancellationCausal!');
            });

        BayportService.GetCategory().then(function (d) {
            $scope.CategoryList = d.data;
        },
            function (error) {
                alert('Error GetCategory!');
            });

        BayportService.GetBranches().then(function (d) {
            $scope.BranchesList = d.data;
        },
            function (error) {
                alert('Error GetBranches!');
            });

        BayportService.GetRegionals().then(function (d) {
            $scope.RegionalsList = d.data;
        },
            function (error) {
                alert('Error GetRegionals!');
            });

        BayportService.GetCoordinators().then(function (d) {
            $scope.CoordinatorsList = d.data;
        },
            function (error) {
                alert('Error GetCoordinators!');
            });

        BayportService.GetExecutiveType().then(function (d) {
            $scope.ExecutiveTypeList = d.data;
        },
            function (error) {
                alert('Error GetExecutiveType!');
            });

        BayportService.GetChannelType().then(function (d) {
            $scope.ChannelTypeList = d.data;
        },
            function (error) {
                alert('Error GetChannelType!');
            });

        BayportService.GetSalesChannel().then(function (d) {
            $scope.SalesChannelList = d.data;
        },
            function (error) {
                alert('Error GetSalesChannel!');
            });

        BayportService.GetLisDocuments().then(function (d) {
            $scope.LisDocuments = d.data;
        },
            function (error) {
                alert('Error GetLisDocuments!');
            });

    };

    $scope.GetCities = function () {
        $scope.actualizacion.DepartamentoID = $scope.actualizacion.depto;

        BayportService.GetCities($scope.actualizacion.depto).then(function (d) {
            $scope.CitiesList = d.data;
        }, function (error) {
            alert('Error GetCities!');
        });
    };

    $scope.GetNeighborhood = function () {
        $scope.actualizacion.MunicipalityID = $scope.actualizacion.ciudad;

        BayportService.GetNeighborhood($scope.actualizacion.ciudad).then(function (d) {
            $scope.NeighborhoodList = d.data;
        }, function (error) {
            alert('Error GetCities!');
        });
    };

    $scope.GetExecutiveInformation = function () {
        BayportService.GetExecutiveInformation().then(function (d) {
            $scope.dataExecutive = d.data;
            //Se precarga informacion
            $scope.actualizacion.CodEjecutivo = d.data.executiveCode;
            $scope.actualizacion.TipoEjecutivo = d.data.executiveType;
            $scope.actualizacion.fechaCreacion = d.data.creationDate;
            $scope.actualizacion.EnListas = d.data.blackLists;
            $scope.actualizacion.Estado = d.data.status;

            $scope.actualizacion.Sucursal = d.data.branchCode;
            $scope.actualizacion.CodCoordinador = d.data.bossCode;
            $scope.actualizacion.correoBayport = d.data.bayportEmail;
            $scope.actualizacion.JefeZona = d.data.bossCode;
            $scope.actualizacion.TipoCanal = d.data.channelType;


            $scope.actualizacion.CodCanal = d.data.codCnlVta;
            $scope.actualizacion.tipoDoc = d.data.documentType;
            $scope.actualizacion.numDocto = d.data.documentID;
            $scope.actualizacion.numDoctoCargado = d.data.documentID.toLocaleString();
            $scope.actualizacion.nombre1 = d.data.name1;
            $scope.actualizacion.nombre2 = d.data.name2;
            $scope.actualizacion.apellido1 = d.data.surname1;
            $scope.actualizacion.apellido2 = d.data.surname2;
            $scope.actualizacion.lugarExp = d.data.expeditionPlace;
            $scope.actualizacion.fechaExpedicion = d.data.expeditionDate;
            $scope.actualizacion.lugarNac = d.data.placeBirth;
            $scope.actualizacion.fechaNacimiento = d.data.birthDate;
            $scope.actualizacion.gender = d.data.gender;
            $scope.actualizacion.civilStatus = d.data.civilStatus;
            $scope.actualizacion.personasCargo = d.data.peopleInCharge;
            $scope.actualizacion.tipoVivienda = d.data.housingType;
            $scope.actualizacion.dirNotifica = d.data.notifyAddress;
            $scope.actualizacion.depto = d.data.department;
            $scope.GetCities();
            $scope.actualizacion.ciudad = (d.data.neighborhood).toString().substring(0, 5);
            $scope.GetNeighborhood();
            $scope.actualizacion.barrio = d.data.neighborhood;
            $scope.actualizacion.celular = d.data.executivePhone;
            $scope.actualizacion.correo = d.data.email;

            $scope.actualizacion.telFijo = d.data.housePhone;
            $scope.actualizacion.estudios = d.data.appliedStudies;
            //if (d.data.spouseID != "0") {
            $scope.actualizacion.nombreConyuge = d.data.spouseName;
            $scope.actualizacion.celularConyuge = d.data.spouseCellphone;
            $scope.actualizacion.emailConyuge = d.data.spouseEmail;
            $scope.actualizacion.cedulaConyuge = d.data.spouseID;

            $scope.actualizacion.activos = d.data.assets;
            $scope.actualizacion.activosCargado = d.data.assets.toLocaleString();
            $scope.actualizacion.pasivos = d.data.liabilities;
            $scope.actualizacion.pasivosCargado = d.data.liabilities.toLocaleString();
            $scope.actualizacion.ingresos = d.data.income;
            $scope.actualizacion.ingresosCargado = d.data.income.toLocaleString();
            $scope.actualizacion.gastos = d.data.expenses;
            $scope.actualizacion.gastosCargado = d.data.expenses.toLocaleString();
            $scope.actualizacion.otrosIngresos = d.data.otherIncome;
            $scope.actualizacion.otrosIngresosCargado = d.data.otherIncome.toLocaleString();
            $scope.actualizacion.descOtrosIng = d.data.otherIncomeDescription;

            $scope.actualizacion.AFP = d.data.afpNIT;
            $scope.actualizacion.ARP = d.data.arpNIT;
            $scope.actualizacion.EPS = d.data.epsNIT;

            $scope.actualizacion.tipoCuenta = "" + d.data.accountType + "";
            $scope.actualizacion.Banco = "" + d.data.entityBank + "";
            $scope.actualizacion.numeroCuenta = d.data.bankAccount;
            $scope.actualizacion.Regional = d.data.regionalCode;
            $scope.actualizacion.CausalCancelacion = "" + d.data.causeCancelation + "";
            $scope.actualizacion.CategoriaEjecutivo = "" + d.data.executiveCategory + "";

        }, function (error) {
            alert('Error GetExecutiveInformation!');
        });
    };

    $scope.uploadedFile = function (element) {
        $scope.$apply(function ($scope) {
            $scope.files = element.files;
            var boton = element.id;
            var prueba = "#" + boton;
            var nombreArchivo = element.files["0"].name;
            var nombreLabel = "#data-" + boton;
            $(nombreLabel).text(nombreArchivo);
        });
    }



    $scope.procesarFormularioActualizacion = function (valido) {
        valido = true;
        $scope.files = [];
        if (valido) {
            // console.log($scope.actualizacion);

            BayportService.ActualizarAsesor($scope.actualizacion).then(function (d) {
                //$scope.oferta = d.data;
                if (d.data.errorCode !== "0") {
                    alert(d.data.errorMessage);
                }
                else {
                    var fdata = new FormData();
                    //Se recorre la lista de documentos
                    var archivo;
                    for (var i = 0; i < $scope.LisDocuments.lstParamDocuments.length; i++) {
                        if (document.getElementById($scope.LisDocuments.lstParamDocuments[i].DocumentType).files[0] != undefined) {
                            archivo = document.getElementById($scope.LisDocuments.lstParamDocuments[i].DocumentType).files[0];
                            fdata.append($scope.LisDocuments.lstParamDocuments[i].DocumentType, archivo);
                            $scope.files.push(archivo);
                        }
                    }
                    fdata.append('files', $scope.files);

                    BayportService.SubirArchivos(fdata).then(function (e) {
                        console.log(e);
                    }, function (error) {
                        console.log(error);
                    });

                    alert("Información actualizada con éxito");
                    $window.location.href = '/UpdateExecutive/index';
                }
            }, function (error) {
                $location.path('/Error/?error=505');
            });
        } else {
            alert('Por favor Diligencie formulario correctamente!');
        }
    };
});

app.controller('ComisionesController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";
    $scope.settingsDropDownList = {
        scrollableHeight: '200px',
        scrollable: true,
        enableSearch: false,
        dynamicTitle: false,
        showUncheckAll: false,
        showCheckAll: false,
        buttonClasses: 'btn btn-info boton-multiselect',
        displayAllSelectedText: true
    };

    $scope.settingsDropDownTexts = {
        buttonDefaultText: 'Seleccione Asesor'
    };

    //$scope.Childs = [{ id: 0, label: 'NA' }];
    $scope.lstExecutiveChildsSelected = [];

    $scope.ConsultarComisiones = function (tipo) {
        $scope.EncabezadoComisiones = {};
        $scope.MovimientosComisiones = {};
        $scope.SaldosComisiones = {};
        // $scope.commisions = {};

        $scope.visible = false;
        $scope.cabeceras = false;

        $scope.childIds = [];
        $scope.childSelected = {};
        //angular.forEach($scope.lstExecutiveChildsSelected, function (value, index) {
        //    $scope.childIds.push(value.id);
        //});
        if ($scope.PQR !== undefined) {
            $scope.childSelected = $scope.PQR.codigoHijo
            $scope.childIds.push($scope.childSelected);
        }



        BayportService.GetHeaderCommisions($scope.childSelected, tipo).then(function (d) {
            $scope.EncabezadoComisiones = d.data;
            console.log($scope.EncabezadoComisiones);
            $scope.accountCode = d.data.lstCommissionHeader["0"].accountCode;
            $scope.visible = true;
            if ($scope.commisions != undefined || $scope.childIds !== undefined) {
                BayportService.GetMovesCommissions($scope.accountCode, $scope.commisions.nroCredito, tipo, $scope.commisions.fecIni, $scope.commisions.fecFin, $scope.childIds).then(function (d) {
                    $scope.MovimientosComisiones = d.data;
                    console.log($scope.MovimientosComisiones);
                }, function (error) {
                    alert('Error ConsultarComisiones.GetMovesCommissions!');
                })

                BayportService.GetBalancesCommissions($scope.accountCode, $scope.childSelected, tipo).then(function (d) {
                    $scope.SaldosComisiones = d.data;
                    console.log($scope.SaldosComisiones);
                }, function (error) {
                    alert('Error ConsultarComisiones.GetBalancesCommissions!');
                })

                BayportService.GetUploadDocuments().then(function (d) {
                    $scope.UploadDocuments = d.data;
                    console.log($scope.UploadDocuments);
                }, function (error) {
                    alert('Error ConsultarComisiones.GetUploadDocuments!');
                })
            }


        }, function (error) {
            alert('Error ConsultarComisiones.GetHeaderCommisions!');
        });
    };

    $scope.LimpiarComisiones = function () {
        $scope.EncabezadoComisiones = {};
        $scope.MovimientosComisiones = {};
        $scope.SaldosComisiones = {};
        $scope.accountCode = "";
        if ($scope.commisions.nroCredito != undefined)
            $scope.commisions.nroCredito = "";
        $scope.commisions.fecIni = "";
        $scope.commisions.fecFin = "";
        $scope.PQR = {};
    };

    $scope.GetExecutiveLevel = function () {
        BayportService.GetExecutiveLevel().then(function (d) {
            $scope.Levels = d.data;
            //console.log($scope.Levels);
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };

    $scope.GetExecutiveChilds = function () {
        BayportService.GetExecutiveChilds($scope.PQR.codigoNivel).then(function (d) {
            $scope.Childs = d.data.lstExecutiveChilds;
            //console.log($scope.Childs);
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };
});

app.controller('PQRController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";
    //$scope.PQRCreate = {};
    $scope.mostrarLista = false;


    $scope.settingsDropDownList = {
        scrollableHeight: '200px',
        scrollable: true,
        enableSearch: false,
        dynamicTitle: false,
        showUncheckAll: false,
        showCheckAll: false,
        buttonClasses: 'btn btn-info boton-multiselect',
        displayAllSelectedText: true
    };

    $scope.settingsDropDownTexts = {
        buttonDefaultText: 'Seleccione Asesor'
        //    //checkAll: 'Seleccionar todo',
        //    //uncheckAll: 'Desmarcar todo',
        //    //checked: 'item selected',
        //    //checkedPlural: 'items selected',
        //    //searchPlaceholder: 'Find',
        //    //searchEmptyResult: 'Nothing found...',
        //    //searchNoRenderText: 'Type in search box to see results...',
        //    --,
        //    //allSelected: 'All selected',
    };

    //$scope.Childs = [{ id : 0, label : 'NA'}];
    $scope.lstExecutiveChildsSelected = [];

    $scope.LimpiarPQR = function () {
        $scope.EncabezadoPQR = {};
        $scope.LogPQR = {};
        $scope.NovedadesPQR = {};
        if ($scope.PQR.nroPQR != undefined)
            $scope.PQR.nroPQR = "";

        if ($scope.PQR.nroCredito != undefined)
            $scope.PQR.nroCredito = "";

        if ($scope.PQR.codigoNivel != undefined)
            $scope.PQR.codigoNivel = "";

        if ($scope.PQR.codigoHijo != undefined)
            $scope.PQR.codigoHijo = "";


        $scope.PQR.fecIni = "";
        $scope.PQR.fecFin = "";
        $scope.PQR.codigoHijo = "";
        $scope.PQR = {};
    };

    $scope.ConsultarPQR = function (tipo) {
        $scope.EncabezadoPQR = {};
        $scope.LogPQR = {};
        $scope.NovedadesPQR = {};

        $scope.visible = false;
        //$scope.cabeceras = false;


        var childIds = [];
        /*  angular.forEach($scope.lstExecutiveChildsSelected, function (value, index) {
              childIds.push(value.id);
          });*/
        childIds.push($scope.PQR.codigoHijo);


        BayportService.GetSummaryPQR($scope.PQR.codigoHijo, tipo).then(function (d) {
            $scope.SummaryPQR = d.data;
        }, function (error) {
            alert('Error GetSummaryPQR!');
        });

        BayportService.GetHeaderPQR(tipo, $scope.PQR.fecIni, $scope.PQR.fecFin, $scope.PQR.nroCredito, $scope.PQR.nroPQR, $scope.PQR.TipoFlujo,
            $scope.PQR.Estado, childIds).then(function (d) {
                $scope.EncabezadoPQR = d.data;
                //$scope.processNumber = d.data.lstHeaderPQR["0"].processNumber;
                $scope.visible = true

            }, function (error) {
                alert('Error PQRController.GetHeaderPQR!');
            });

        $scope.ConsultarDetallePQR = function (index) {
            BayportService.GetNoveltyPQR($scope.EncabezadoPQR.lstHeaderPQR[index].processNumber).then(function (d) {
                $scope.NovedadesPQR = d.data;
                //console.log($scope.NovedadesPQR);
            }, function (error) {
                alert('Error PQRController.GetNoveltyPQR!');
            });
            BayportService.GetLogPQR($scope.EncabezadoPQR.lstHeaderPQR[index].processNumber).then(function (d) {
                $scope.LogPQR = d.data;
                // console.log($scope.LogPQR);
            }, function (error) {
                alert('Error PQRController.GetLogPQR!');
            });
        };
    };

    $scope.GetFlow = function () {
        BayportService.GetFlow().then(function (d) {
            $scope.Flows = d.data;
        }, function (error) {
            alert('Error GetFlow!');
        });
    };

    $scope.GetStates = function () {
        BayportService.GetStates().then(function (d) {
            $scope.States = d.data;
        }, function (error) {
            alert('Error GetStates!');
        });
    };

    $scope.GetJustify = function () {
        BayportService.GetJustification($scope.PQR.TipoFlujo).then(function (d) {
            $scope.JustifyList = d.data;
        }, function (error) {
            alert('Error GetJustify!');
        });
    };

    $scope.ValidateLoanNumber = function () {
        if ($scope.PQR.NumeroCredito != "" && $scope.PQR.NumeroCredito != undefined && $scope.PQR.NumeroCredito != null) {
            BayportService.GetLoanResume($scope.PQR.NumeroCredito).then(function (d) {
                $scope.loadResume = d.data;
                if ($scope.loadResume === undefined || $scope.loadResume.loanNumber <= 0) {
                    alert('No existe un crédito con el número ingresado');
                }
            }, function (error) {
                alert('Error GetLoanResume!');
            });
        }
    };

    $scope.procesarFormularioPQR = function (valido) {
        //valido = true;
        if (valido) {

            console.log($scope.PQR);


            var message = "ESTÁ SEGURO DE RADICAR UN PQRS CON LA INFORMACIÓN INGRESADA?  \n";
            if ($scope.loadResume != undefined && $scope.loadResume.loanNumber > 0) {
                message = message + " Número de Crédito No.: " + $scope.PQR.NumeroCredito + " \n";
                message = message + " Monto: " + $scope.loadResume.amount.toLocaleString() + " \n";
                message = message + " Valor Desembolsado: " + $scope.loadResume.disbursement.toLocaleString() + " \n";
                message = message + " Sucursal: " + $scope.loadResume.branchCode + " - " + $scope.loadResume.branchName + " \n";
            }

            create = $window.confirm(message);

            if (create) {
                BayportService.CreatePQR($scope.PQR).then(function (d) {
                    console.log(d.data);
                    alert(d.data.errorMessage);
                    $scope.PQR = {};
                    $scope.loadResume = {};
                }, function (error) {
                    $location.path('/Error/?error=505');
                });
            }
        } else {
            alert('Por favor Diligencie formulario correctamente!');
        }
    };




    $scope.GetExecutiveLevel = function () {
        $scope.mostrarLista = false;
        BayportService.GetExecutiveLevel().then(function (d) {
            $scope.Levels = d.data;
            $scope.mostrarLista = true;
            //console.log($scope.Levels);
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };

    $scope.GetExecutiveChilds = function () {
        BayportService.GetExecutiveChilds($scope.PQR.codigoNivel).then(function (d) {
            $scope.Childs = d.data.lstExecutiveChilds;
            // console.log($scope.Childs);
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };

});

app.controller('SimulatorController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";
    $scope.simularCategoria = function (valido) {
        $scope.simulador.categoriaNew = '';
        $scope.simulador.feeNew = '';
        $scope.simulador.feeRenovated = '';
        valido = true;
        if (valido) {
            console.log($scope.simulador);
            BayportService.SimulateCategory($scope.simulador.valor).then(function (d) {
                //                console.log(d.data);
                $scope.simulador.categoriaNew = d.data.categoryName;
                $scope.simulador.feeNew = d.data.feeNew;
                $scope.simulador.feeRenovated = d.data.feeRenovated;
            }, function (error) {
                $location.path('/Error/?error=505');
            });

        } else {
            alert('Por favor Diligencie formulario correctamente!');
        }
    };
});

app.controller('FolioController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";
    $scope.folio = {};

    $scope.GetExecutiveLevel = function () {
        BayportService.GetExecutiveLevel().then(function (d) {
            $scope.Levels = d.data;
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };

    $scope.GetExecutiveChilds = function () {
        BayportService.GetExecutiveChilds($scope.folio.codigoNivel).then(function (d) {
            $scope.Childs = d.data.lstExecutiveChilds;
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };

    $scope.LimpiarFolio = function () {
        if ($scope.folio.nroFolio != undefined)
            $scope.folio.nroFolio = "";

        if ($scope.folio.codigoNivel != undefined)
            $scope.folio.codigoNivel = "";

        if ($scope.folio.codigoHijo != undefined)
            $scope.folio.codigoHijo = "";


        $scope.folio.fecIni = "";
        $scope.folio.fecFin = "";
        $scope.folio.codigoHijo = "";
        $scope.folio = {};
        $scope.detailFolios = {};
    };

    $scope.ConsultarFolio = function (tipo) {

        $scope.detailFolios = {};
        BayportService.GetFolioDetail(tipo, $scope.folio.fecIni, $scope.folio.fecFin, $scope.folio.nroFolio, $scope.folio.codigoHijo).then(function (d) {
            $scope.detailFolios = d.data.loanFolioDetail;
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };
});

app.controller('ComplianceGoalController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";
    $scope.child;
    $scope.ProximaCategoria = {};
    $scope.ProximaCategoria.amount = 0;


    BayportService.GetHeaderCommisions($scope.child, 1).then(function (d) {
        $scope.EncabezadoComisiones = d.data;
    }, function (error) {
        alert('Error ComplianceGoalController.GetHeaderCommisions!');
    });

    BayportService.GetNextCategory().then(function (d) {
        $scope.ProximaCategoria = d.data;
        //console.log($scope.ProximaCategoria);
    }, function (error) {
        alert('Error ComplianceGoalController.GetNextCategory!');
    });

    BayportService.GetAccumulatedLoan().then(function (d) {
        $scope.AccumulatedLoan = d.data;
        console.log($scope.AccumulatedLoan);
        $scope.colors = ['#45b7cd', '#ff6384', '#ff8e72'];

        $scope.arrayMeses = [];
        $scope.arrayValor = [];

        for (let i = 0; i < $scope.AccumulatedLoan.lstAccumulatedLoan.length; i++) {
            $scope.arrayMeses.push($scope.AccumulatedLoan.lstAccumulatedLoan[i].month);
            $scope.arrayValor.push($scope.AccumulatedLoan.lstAccumulatedLoan[i].amount);

        }

        $scope.labels = $scope.arrayMeses;
        $scope.data = [
            $scope.arrayValor,
            $scope.arrayValor
        ];
        $scope.datasetOverride = [
            {
                label: "Bar chart",
                borderWidth: 1,
                type: 'bar'
            },
            {
                label: "Line chart",
                borderWidth: 3,
                hoverBackgroundColor: "rgba(255,99,132,0.4)",
                hoverBorderColor: "rgba(255,99,132,1)",
                type: 'line'
            }
        ];


    }, function (error) {
        alert('Error ComplianceGoalController.GetAccumulatedLoan!');
    });

    BayportService.GetAccumulatedClarifications().then(function (d) {
        $scope.AccumulatedClarifications = d.data;
        //console.log($scope.AccumulatedClarifications);
    }, function (error) {
        alert('Error ComplianceGoalController.GetAccumulatedClarifications!');
    });

    BayportService.GetGoalExecutive().then(function (d) {
        $scope.GoalExecutive = d.data;
        $scope.arrayCumplimiento = [];

        $scope.cumplimiento = 0;

        $scope.vlrCumplimiento = (($scope.GoalExecutive.goalExecutive.complianceValue * 100) / $scope.GoalExecutive.goalExecutive.goalValue);
        $scope.arrayCumplimiento.push($scope.vlrCumplimiento);
        $scope.arrayCumplimiento.push(100 - $scope.vlrCumplimiento);

        $scope.labelsCumpl = ["Cumplido", "Pendiente"];
        $scope.dataCump = $scope.arrayCumplimiento;

        $scope.colors = ['#45b7cd', '#ff6384', '#ff8e72'];

    }, function (error) {
        alert('Error ComplianceGoalController.GetGoalExecutive!');
    });


    BayportService.GetProductivity().then(function (d) {
        $scope.Productividad = d.data;
        //  console.log($scope.Productividad);
    }, function (error) {
        alert('Error ComplianceGoalController.GetProductivity!');
    });

    BayportService.GetCategoryRange().then(function (d) {
        $scope.RangoCategoria = d.data;
    }, function (error) {
        alert('Error ComplianceGoalController.GetCategoryRange!');
    });



    BayportService.GetGoalSupervisor().then(function (d) {
        $scope.GoalSupervisor = d.data;
        console.log($scope.GoalSupervisor);
        $scope.arrayCumplimientoS = [];

        $scope.cumplimiento = 0;

        $scope.vlrCumplimientoS = (($scope.GoalSupervisor.goalSupervisor.complianceValue * 100) / $scope.GoalSupervisor.goalSupervisor.goalValue);
        $scope.arrayCumplimientoS.push($scope.vlrCumplimientoS);
        $scope.arrayCumplimientoS.push(100 - $scope.vlrCumplimientoS);


        $scope.labelsCumplS = ["Cumplido", "Pendiente"];
        $scope.dataCumpS = $scope.arrayCumplimientoS;

        $scope.colors = ['#45b7cd', '#ff6384', '#ff8e72'];

    }, function (error) {
        alert('Error ComplianceGoalController.GetGoalSupervisor!');
    });



    $scope.GetExecutiveLevel = function () {
        BayportService.GetExecutiveLevel().then(function (d) {
            $scope.Levels = d.data;
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };

    $scope.GetExecutiveChilds = function () {
        BayportService.GetExecutiveChilds($scope.Avance.codigoNivel).then(function (d) {
            $scope.Childs = d.data.lstExecutiveChilds;
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };

    $scope.ConsultarAvance = function () {
        BayportService.GetProgress($scope.Avance.fecIni, $scope.Avance.fecFin, $scope.Avance.codigoHijo).then(function (d) {
            $scope.Progress = d.data;
            //console.log($scope.Progress);
        }, function (error) {
            alert('Error GetExecutiveLevel!');
        });
    };


});

app.controller('ParametrosController', function ($scope, BayportService, $filter, $location, $window) {
    $window.localStorage["formulario"] = "";
    $scope.regexPlazos = /^(?=.*[0-9])(?=.*\d)?([0-9]\,?)*[0-9]$/;
    $scope.regexOocionesCampos = /^(?=.*[0-9a-zA-Z])(?=.*\d)?([A-Za-z0-9]\,?)*[0-9A-Za-z]$/;
    $scope.NoSpecialCharacters = /^[A-Za-zñÑ0-9\\s]+$'/; //'^[a-zA-Z0-9]+$';
    $scope.regexEmail = /^[^@]+@[^@]+\.[a-zA-Z]{2,}$/
    $scope.objEliminar = {};
    $scope.estadoActivo = [0, "Inactivo"];
    $scope.estadoInactivo = [1, "Activo"];
    $scope.opcionesEstado = [$scope.estadoActivo, $scope.estadoInactivo];

    $scope.Parametro = {};
    $scope.nombreArchivo = "";
    $scope.tiposolicitud = {
        ids: {}
    }
    $scope.newSolicitud = {};
    $scope.validerror = true;
    $scope.validmsgerror = "";

    $scope.objDelete = function (item) {
        $scope.objEliminar = item;
    }
    $scope.openModal = function (modal) {
        console.log('abrir');
        $('#' + modal).modal('show');
    }
    $scope.closeModal = function (modal) {
        console.log('cerrar')
        $('#' + modal).modal('toggle');
    }
    $scope.eliminaTexto = function (element) {
        $('#' + element).removeClass('alert alert-danger');
        $('#' + element).text('');
    }
    //Dependencias
    $scope.getDependenciasActivas = function () {
        BayportService.getDependenciasActivas().then(function (d) {
            $scope.dependenciasList = d.data;
        });
    }
    $scope.saveDependencias = function () {
        if ($scope.newDependencia && $scope.newDependencia.codigo && $scope.newDependencia.nombre) {
            BayportService.saveDependencia($scope.newDependencia.codigo, $scope.newDependencia.nombre, $scope.newDependencia.estado).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.newDependencia = {};
                    $scope.closeModal('myModalNueva');
                    $scope.getDependencias();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe diligencias todo los campos');
        }
    }
    $scope.getDependencias = function () {
        BayportService.getDependencias().then(function (d) {
            $scope.dependencias = d.data;
        });
    }
    $scope.updDependencia = function (id, nombre, estado) {
        BayportService.updDependencia(id, nombre, estado).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.getDependencias();
                $scope.closeModal('myModalEdit');
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteDependencia = function (id) {
        BayportService.deleteDependencia(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getDependencias();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.loadDependencia = function (id) {
        BayportService.loadDependencia(id).then(function (d) {
            $scope.dependencia = d.data;
            if ($scope.dependencia.msg.errorCode != '200') {
                alert($scope.dependencia.msg.errorMessage);
            }
            else {
                $scope.dependencia = $scope.dependencia.ListDependencias[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    //Periodos
    $scope.getPeriodos = function () {
        BayportService.getPeriodos().then(function (d) {
            $('#SelectPeriodo').removeClass('alert alert-danger');
            $scope.eliminaTexto('periodoPlazo');
            $scope.Periodos = d.data;
        });
    }
    $scope.getIdPeriodo = function (id) {
        BayportService.getIdPeriodos(id).then(function (d) {
            $scope.Periodo = d.data;
            if ($scope.Periodo.msg.errorCode != '200') {
                alert($scope.Periodo.msg.errorMessage);
            }
            else {
                $scope.Periodo = $scope.Periodo.ListPeriodos[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.savePeriodo = function () {
        BayportService.savePeriodos($scope.newPeriodo.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newPeriodo = {};
                $scope.closeModal('myModalNueva');
                $scope.getPeriodos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updPeriodo = function (id, periodo) {
        BayportService.updPeriodos(id, periodo).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Periodo = {};
                $scope.closeModal('myModalEdit');
                $scope.getPeriodos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deletePeriodo = function (id) {
        BayportService.deletePeriodos(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getPeriodos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    //Plazos
    $scope.getPlazos = function () {
        BayportService.getPlazos().then(function (d) {
            $scope.plazos = d.data;
        });
    }
    $scope.getIdPlazo = function (id) {
        BayportService.getIdPlazo(id).then(function (d) {
            $scope.plazo = d.data;
            if ($scope.plazo.msg.errorCode != '200') {
                alert($scope.plazo.msg.errorMessage);
            }
            else {
                $scope.plazo = $scope.plazo.ListPlazos[0];
                if ($scope.plazo.periodo) {
                    $scope.plazo.periodo = Number($scope.plazo.periodo);
                }
                console.log($scope.plazo);
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.updPlazo = function (id, plazo) {
        var plazos = String(plazo).split(';');
        var repetidos = 0;
        repetidos = $scope.findrepetido(plazos);
        console.log(repetidos)
        if (repetidos == 0) {
            BayportService.updPlazo(id, plazo).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.plazo = {};
                    $scope.closeModal('myModalEdit');
                    $scope.getPlazos();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        }
        else {
            console.log('hay repetidos')
            $("#EditplazoInvalido").addClass('alert alert-danger');
            $('#EditplazoInvalido').text('No se pueden repetir números');
        }
    }
    $scope.deletePlazo = function (id) {
        BayportService.deletePlazo(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.getPlazos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.savePlazo = function () {
        var plazos = String($scope.newPlazo.nombre).split(';');
        var repetidos = 0;
        repetidos = $scope.findrepetido(plazos);
        if (repetidos == 0) {
            BayportService.savePlazo($scope.newPlazo.nombre, $scope.newPlazo.periodo).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.newPlazo = {};
                    $scope.closeModal('myModalNueva');
                    $scope.getPlazos();
                } else if (d.data.errorCode == '200') {
                    alert(d.data.errorMessage);
                    $('#SelectPeriodo').addClass('alert alert-danger');
                    $('#periodoPlazo').addClass('alert alert-danger');
                    $('#periodoPlazo').text('Debe seleccionar otro periodo');
                } else {
                    alert(d.data.errorMessage);
                    $scope.closeModal('myModalNueva');
                }
            }, function (err) {
                alert(err);
                $scope.closeModal('myModalNueva');
            });
        } else {
            $("#plazoInvalido").addClass('alert alert-danger');
            $('#plazoInvalido').text('No se pueden repetir números');
        }
    }
    $scope.findrepetido = function (arr) {
        var repetidos = 0;
        for (var i = 0; i < arr.length - 1; i++) {
            for (var j = 0; j < arr.length; j++) {
                if (arr[i] == arr[j] && i != j) {
                    repetidos += 1;
                    break;
                }
            }
        }
        return repetidos;
    }
    //Descuentos
    $scope.getDescuentos = function () {
        BayportService.getDescuentos().then(function (d) {
            $scope.descuentos = d.data;
        });
    }
    $scope.getIdDescuento = function (id) {
        BayportService.getIdDescuento(id).then(function (d) {
            $scope.descuento = d.data;
            if ($scope.descuento.msg.errorCode != '200') {
                alert($scope.descuento.msg.errorMessage);
            }
            else {
                $scope.descuento = $scope.descuento.ListQuincenaDscto[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.updDescuento = function (id, descuento) {
        BayportService.updDescuento(id, descuento).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.descuento = {};
                $scope.closeModal('myModalEdit');
                $scope.getDescuentos();
                $scope.myModalEdit = false;
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteDescuento = function (id) {
        BayportService.deleteDescuento(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getDescuentos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.saveDescuento = function () {
        BayportService.saveDescuento($scope.newdescuento.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newdescuento = {};
                $scope.closeModal('myModalNueva');
                $scope.getDescuentos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }

    // DESTINO _CRÉDITO
    $scope.getDestino = function () {
        BayportService.getDestino().then(function (d) {
            $scope.destinos = d.data;
        });
    }
    $scope.getIdDestino = function (id) {
        BayportService.getIdDestinoCredito(id).then(function (d) {
            $scope.destino = d.data;
            if ($scope.destino.msg.errorCode != '200') {
                alert($scope.destino.msg.errorMessage);
            }
            else {
                $scope.destino = $scope.destino.ListDestinoCred[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveDestino = function () {
        BayportService.saveDestinoCredito($scope.newDestino.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newDestino = {};
                $scope.closeModal('myModalNueva');
                $scope.getDestino();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updDestino = function (id, destino) {
        BayportService.updDestinoCredito(id, destino).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.destino = {};
                $scope.closeModal('myModalEdit');
                $scope.getDestino();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteDestino = function (id) {
        BayportService.deleteDestinoCredito(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getDestino();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }

    //Empresa Telefónica

    $scope.getEmpresaTelefonica = function () {
        BayportService.getEmpresaTelefonica().then(function (d) {
            $scope.EmpresasTelefonica = d.data;
        });
    }
    $scope.getIdEmpresaTelefonica = function (id) {
        BayportService.getIdEmpresaTelefonica(id).then(function (d) {
            $scope.EmpresaTelefonica = d.data;
            if ($scope.EmpresaTelefonica.msg.errorCode != '200') {
                alert($scope.EmpresaTelefonica.msg.errorMessage);
            }
            else {
                $scope.EmpresaTelefonica = $scope.EmpresaTelefonica.ListEmpresas[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveEmpresaTelefonica = function () {
        BayportService.saveEmpresaTelefonica($scope.newEmpresaTelefonica.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newEmpresaTelefonica = {};
                $scope.closeModal('myModalNueva');
                $scope.getEmpresaTelefonica();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updEmpresaTelefonica = function (id, empresa) {
        BayportService.updEmpresaTelefonica(id, empresa).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.EmpresaTelefonica = {};
                $scope.closeModal('myModalEdit');
                $scope.getEmpresaTelefonica();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteEmpresaTelefonica = function (id) {
        BayportService.deleteEmpresaTelefonica(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getEmpresaTelefonica();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }

    // Estado Civil

    $scope.getEstadoCivil = function () {
        BayportService.getEstadoCivil().then(function (d) {
            $scope.estadosCiviles = d.data;
        });
    }
    $scope.getIdEstadoCivil = function (id) {
        BayportService.getIdEstadoCivil(id).then(function (d) {
            $scope.EstadoCivil = d.data;
            if ($scope.EstadoCivil.msg.errorCode != '200') {
                alert($scope.EstadoCivil.msg.errorMessage);
            }
            else {
                $scope.EstadoCivil = $scope.EstadoCivil.ListEstados[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveEstadoCivil = function () {
        BayportService.saveEstadoCivil($scope.newEstadoCivil.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newEstadoCivil = {};
                $scope.closeModal('myModalNueva');
                $scope.getEstadoCivil();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updEstadoCivil = function (id, estadoCivil) {
        BayportService.updEstadoCivil(id, estadoCivil).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.EstadoCivil = {};
                $scope.closeModal('myModalEdit');
                $scope.getEstadoCivil();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteEstadoCivil = function (id) {
        BayportService.deleteEstadoCivil(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getEstadoCivil();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }

    // Tipos de Identificacion
    $scope.getTipoIdentificacion = function () {
        BayportService.getTiposIdentificacion().then(function (d) {
            $scope.tiposIdentificacion = d.data;
        });
    }
    $scope.getIdTipoIdentificacion = function (id) {
        BayportService.getIdTiposIdentificacion(id).then(function (d) {
            $scope.TipoIdentificacion = d.data;
            if ($scope.TipoIdentificacion.msg.errorCode != '200') {
                alert($scope.TipoIdentificacion.msg.errorMessage);
            }
            else {
                $scope.TipoIdentificacion = $scope.TipoIdentificacion.ListTipoIdentificacion[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveTipoIdentificacion = function () {
        BayportService.saveTiposIdentificacion($scope.newTipoIdentificacion.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newTipoIdentificacion = {};
                $scope.closeModal('myModalNueva');
                $scope.getTipoIdentificacion();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updTipoIdentificacion = function (id, identificacion) {
        BayportService.updTiposIdentificacion(id, identificacion).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.TipoIdentificacion = {};
                $scope.closeModal('myModalEdit');
                $scope.getTipoIdentificacion();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteTipoIdentificacion = function (id) {
        BayportService.deleteTiposIdentificacion(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getTipoIdentificacion();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    // Ingresos Neto
    $scope.getIngresos = function () {
        BayportService.getIngresos().then(function (d) {
            $scope.IngresosNeto = d.data;
        });
    }
    $scope.getIdIngresos = function (id) {
        BayportService.getIdIngresos(id).then(function (d) {
            $scope.Ingresos = d.data;
            if ($scope.Ingresos.msg.errorCode != '200') {
                alert($scope.Ingresos.msg.errorMessage);
            }
            else {
                $scope.Ingresos = $scope.Ingresos.ListIngresos[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveIngresos = function () {
        BayportService.saveIngresos($scope.newIngresos.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newIngresos = {};
                $scope.closeModal('myModalNueva');
                $scope.getIngresos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updIngresos = function (id, ingreso) {
        BayportService.updIngresos(id, ingreso).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Ingresos = {};
                $scope.closeModal('myModalEdit');
                $scope.getIngresos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteIngresos = function (id) {
        BayportService.deleteIngresos(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getIngresos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    //Medios
    $scope.getmedioDispo = function () {
        BayportService.getMediosDisposicion().then(function (d) {
            $scope.mediosDispo = d.data;
        });
    }
    $scope.getIdmedioDispo = function (id) {
        BayportService.getIdMediosDisposicion(id).then(function (d) {
            $scope.medioDispo = d.data;
            if ($scope.medioDispo.msg.errorCode != '200') {
                alert($scope.medioDispo.msg.errorMessage);
            }
            else {
                $scope.medioDispo = $scope.medioDispo.ListMedios[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.savemedioDispo = function () {
        BayportService.saveMediosDisposicion($scope.newmedioDispo.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newmedioDispo = {};
                $scope.closeModal('myModalNueva');
                $scope.getmedioDispo();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updmedioDispo = function (id, medio) {
        BayportService.updMediosDisposicion(id, medio).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.medioDispo = {};
                $scope.closeModal('myModalEdit');
                $scope.getmedioDispo();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deletemedioDispo = function (id) {
        BayportService.deleteMediosDisposicion(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getmedioDispo();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }

    //Nomina
    $scope.getNomina = function () {
        BayportService.getTipoNomina().then(function (d) {
            $scope.Nominas = d.data;
        });
    }
    $scope.getIdNomina = function (id) {
        BayportService.getIdTipoNomina(id).then(function (d) {
            $scope.Nomina = d.data;
            if ($scope.Nomina.msg.errorCode != '200') {
                alert($scope.Nomina.msg.errorMessage);
            }
            else {
                $scope.Nomina = $scope.Nomina.ListTipoNomina[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveNomina = function () {
        BayportService.saveTipoNomina($scope.newNomina.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newNomina = {};
                $scope.closeModal('myModalNueva');
                $scope.getNomina();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updNomina = function (id, nomina) {
        BayportService.updTipoNomina(id, nomina).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Nomina = {};
                $scope.closeModal('myModalEdit');
                $scope.getNomina();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteNomina = function (id) {
        BayportService.deleteTipoNomina(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getNomina();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    //Dias Expiracion
    $scope.getDiasExp = function () {
        BayportService.getDiasExp().then(function (d) {
            $scope.DiasExp = d.data;
        });
    }
    $scope.getIdDiasExp = function (dias) {
        BayportService.getIdDiasExp(dias).then(function (d) {
            $scope.DiaExp = d.data;
            if ($scope.DiaExp.msg.errorCode != '200') {
                alert($scope.DiaExp.msg.errorMessage);
            }
            else {
                $scope.DiaExp = $scope.DiaExp.ListDias[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.updDiasExp = function (DiaExp) {
        BayportService.saveDiasExp(DiaExp).then(function (d) {
            if (d.data.errorCode == '200') {
                $scope.DiaExp = {};
                $scope.closeModal('myModalEdit');
                $scope.getDiasExp();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    //Sector
    $scope.getSector = function () {
        BayportService.getSector().then(function (d) {
            $scope.Sectores = d.data;
        });
    }
    $scope.getIdSector = function (id) {
        BayportService.getIdSector(id).then(function (d) {
            $scope.Sector = d.data;
            if ($scope.Sector.msg.errorCode != '200') {
                alert($scope.Sector.msg.errorMessage);
            }
            else {
                $scope.Sector = $scope.Sector.ListSector[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveSector = function () {
        BayportService.saveSector($scope.newSector.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newSector = {};
                $scope.closeModal('myModalNueva');
                $scope.getSector();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updSector = function (id, sector) {
        BayportService.updSector(id, sector).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Sector = {};
                $scope.closeModal('myModalEdit');
                $scope.getSector();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteSector = function (id) {
        BayportService.deleteSector(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getSector();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    //SectorGuias
    $scope.getSectorGuias = function () {
        BayportService.getSectorGuias().then(function (d) {
            $scope.SectoresGuias = d.data;
        });
    }
    $scope.getIdSectorGuias = function (id) {
        BayportService.getIdSectorGuias(id).then(function (d) {
            $scope.SectorGuias = d.data;
            if ($scope.SectorGuias.msg.errorCode != '200') {
                alert($scope.SectorGuias.msg.errorMessage);
            }
            else {
                $scope.SectorGuias = $scope.SectorGuias.ListSectorGuias[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveSectorGuias = function () {
        BayportService.saveSectorGuias($scope.newSectorGuias.nombre, $scope.newSectorGuias.ind_estado).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newSectorGuias = {};
                $scope.closeModal('myModalNueva');
                $scope.getSectorGuias();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updSectorGuias = function (id, sector, estado) {
        BayportService.updSectorGuias(id, sector, estado).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.SectorGuias = {};
                $scope.closeModal('myModalEdit');
                $scope.getSectorGuias();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteSectorGuias = function (id) {
        BayportService.deleteSectorGuias(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getSectorGuias();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    //SectorTablas
    $scope.getSectorTablas = function () {
        BayportService.getSectorTablas().then(function (d) {
            $scope.SectoresTablas = d.data;
        });
    }
    $scope.getIdSectorTablas = function (id) {
        BayportService.getIdSectorTablas(id).then(function (d) {
            $scope.SectorTablas = d.data;
            if ($scope.SectorTablas.msg.errorCode != '200') {
                alert($scope.SectorTablas.msg.errorMessage);
            }
            else {
                $scope.SectorTablas = $scope.SectorTablas.ListSectorTablas[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveSectorTablas = function () {
        BayportService.saveSectorTablas($scope.newSectorTablas.nombre, $scope.newSectorTablas.ind_estado).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newSectorTablas = {};
                $scope.closeModal('myModalNueva');
                $scope.getSectorTablas();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updSectorTablas = function (id, sector, estado) {
        BayportService.updSectorTablas(id, sector, estado).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.SectorTablas = {};
                $scope.closeModal('myModalEdit');
                $scope.getSectorTablas();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteSectorTablas = function (id) {
        BayportService.deleteSectorTablas(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getSectorTablas();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    //Puesto
    $scope.getPuestos = function () {
        BayportService.getPuestos().then(function (d) {
            $scope.Puestos = d.data;
        });
    }
    $scope.getIdPuesto = function (id) {
        BayportService.getIdPuestos(id).then(function (d) {
            $scope.Puesto = d.data;
            if ($scope.Puesto.msg.errorCode != '200') {
                alert($scope.Puesto.msg.errorMessage);
            }
            else {
                $scope.Puesto = $scope.Puesto.ListPuestos[0];
                if ($scope.Puesto.sector) {
                    $scope.Puesto.sector = Number($scope.Puesto.sector);
                }
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.savePuesto = function () {
        BayportService.savePuestos($scope.newPuesto.sector, $scope.newPuesto.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newPuesto = {};
                $scope.closeModal('myModalNueva');
                $scope.getPuestos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updPuesto = function (id, puesto) {
        BayportService.updPuestos(id, puesto).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Puesto = {};
                $scope.closeModal('myModalEdit');
                $scope.getPuestos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deletePuesto = function (id) {
        BayportService.deletePuestos(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getPuestos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }

    // Reca
    $scope.getReca = function () {
        BayportService.getReca().then(function (d) {
            $scope.Recas = d.data;
        });
    }
    $scope.getIdReca = function (id) {
        BayportService.getIdReca(id).then(function (d) {
            $scope.Reca = d.data;
            if ($scope.Reca.msg.errorCode != '200') {
                alert($scope.Reca.msg.errorMessage);
            }
            else {
                $scope.Reca = $scope.Reca.ListRecas[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveReca = function () {
        BayportService.saveReca($scope.newReca.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newReca = {};
                $scope.closeModal('myModalNueva');
                $scope.getReca();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updReca = function (id, reca) {
        BayportService.updReca(id, reca).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Reca = {};
                $scope.closeModal('myModalEdit');
                $scope.getReca();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteReca = function (id) {
        BayportService.deleteReca(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getReca();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    // Sucursales

    $scope.getSucursal = function () {
        BayportService.getSucursales().then(function (d) {
            $scope.Sucursales = d.data;
        });
    }
    $scope.getIdSucursal = function (id) {
        BayportService.getIdSucursales(id).then(function (d) {
            $scope.Sucursal = d.data;
            if ($scope.Sucursal.msg.errorCode != '200') {
                alert($scope.Sucursal.msg.errorMessage);
            }
            else {
                $scope.Sucursal = $scope.Sucursal.ListSucursales[0];
                $scope.getRegionDivision($scope.Sucursal.division);
                if ($scope.Sucursal.tipo_sucursal) {
                    $scope.Sucursal.tipo_sucursal = Number($scope.Sucursal.tipo_sucursal);
                }
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveSucursal = function () {
        if ($scope.newSucursal && $scope.newSucursal.nombre && $scope.newSucursal.identificador &&
            $scope.newSucursal.tipo && $scope.newSucursal.division && $scope.newSucursal.region) {
            $scope.validaEmailCoor($scope.newSucursal.emailCoordinador);
            $scope.validaEmailAux($scope.newSucursal.emailAuxiliar);
            BayportService.saveSucursales($scope.newSucursal.nombre, $scope.newSucursal.identificador,
                $scope.newSucursal.tipo, $scope.newSucursal.division, $scope.newSucursal.region,
                $scope.newSucursal.emailCoordinador, $scope.newSucursal.emailAuxiliar).then(function (d) {
                    if (d.data.errorCode == '0') {
                        $scope.newSucursal = {};
                        $scope.closeModal('myModalNueva');
                        $scope.getSucursal();
                    } else {
                        alert(d.data.errorMessage);
                    }
                });
        } else {
            alert('Debe diligenciar todos los campos');
        }
    }
    $scope.updSucursal = function (id, sucursal, tipo_sucursal, emailCoor, emailAux) {
        $scope.validaEmailCoor(emailCoor);
        $scope.validaEmailAux(emailAux);
        BayportService.updSucursales(id, sucursal, tipo_sucursal, emailCoor, emailAux).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Sucursal = {};
                $scope.closeModal('myModalEdit');
                $scope.getSucursal();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteSucursal = function (id) {
        BayportService.deleteSucursales(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getSucursal();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    // Tipos de Crédito
    $scope.getTipoCredito = function () {
        BayportService.getTipoSolicitud().then(function (d) {
            $scope.tiposSolicitud = d.data;
        });
    }
    $scope.getIdtipoSolicitud = function (id) {
        BayportService.getIdTipoSolicitud(id).then(function (d) {
            $scope.tipoSolicitud = d.data;
            if ($scope.tipoSolicitud.msg.errorCode != '200') {
                alert($scope.tipoSolicitud.msg.errorMessage);
            }
            else {
                $scope.tipoSolicitud = $scope.tipoSolicitud.ListTipoSolicitud[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.savetipoSolicitud = function () {
        BayportService.saveTipoSolicitud($scope.newtipoSolicitud.nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.newtipoSolicitud = {};
                $scope.closeModal('myModalNueva');
                $scope.getTipoCredito();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.updtipoSolicitud = function (id, tipo) {
        BayportService.updTipoSolicitud(id, tipo).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.tipoSolicitud = {};
                $scope.closeModal('myModalEdit');
                $scope.getTipoCredito();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deletetipoSolicitud = function (id) {
        BayportService.deleteTipoSolicitud(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getTipoCredito();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    // Productos
    $scope.getProductos = function () {
        BayportService.getProductos().then(function (d) {
            $scope.productos = d.data;
        },
            function (error) {
                alert(error);
            });
    }
    $scope.getIdProducto = function (id) {
        BayportService.getIdProducto(id).then(function (d) {
            $scope.producto = d.data;
            if ($scope.producto.msg.errorCode != '200') {
                alert($scope.producto.msg.errorMessage);
            }
            else {
                $scope.producto = $scope.producto.ListProductos[0];
                if ($scope.producto.dependencia) {
                    $scope.producto.dependencia = Number($scope.producto.dependencia);
                }
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.updProducto = function (id, producto, estado) {
        BayportService.updProducto(id, producto, estado).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.producto = {};
                $scope.closeModal('myModalEdit');
                $scope.getProductos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteProducto = function (id) {
        BayportService.deleteProducto(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getProductos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.saveProducto = function () {
        if ($scope.newProducto && $scope.newProducto.codigo && $scope.newProducto.dependencia && $scope.newProducto.nombre) {
            BayportService.saveProductos($scope.newProducto.codigo, $scope.newProducto.dependencia, $scope.newProducto.nombre, $scope.newProducto.estado).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.newProducto = {};
                    $scope.closeModal('myModalNueva');
                    $scope.getProductos();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe diligencias todos los campos');
        }
    }
    $scope.getProductoDependencia = function (dependencia) {
        BayportService.getProductosDependencia(dependencia).then(function (d) {
            $scope.productos = d.data;
        },
            function (err) {
                alert(err);
            })
    }
    //Campos Parámterizados
    $scope.ChangeSolicitud = function () {
        $scope.newSolicitud = $filter('filter')($scope.tiposSolicitud.ListTipoSolicitud, { checked: true });
    };
    $scope.getCampos = function () {
        BayportService.getCampos().then(function (d) {
            $scope.Campos = d.data;
        });
    }
    $scope.saveCampos = function () {
        var valid = false;
        if ($scope.newCampo.nombreCampo == 'RECA') {
            $scope.newCampo.tipoDato = 'O';
        }
        if ($scope.productos.ListProductosDependencia.length > 0) {
            valid = ($scope.newCampo.producto) ? true : false;
        } else {
            valid = true;
        }
        if ($scope.newSolicitud.length > 0) {
            valid = true;
        }
        else {
            valid = false;
        }
        if (valid) {
            var cont = 0;
            for (let soli of $scope.newSolicitud) {
                $scope.newCampo.solicitud = soli.secuencia;
                BayportService.saveCampos($scope.newCampo.solicitud, $scope.newCampo.dependencia, $scope.newCampo.producto,
                    $scope.newCampo.periodo, $scope.newCampo.nombreCampo, $scope.newCampo.tipoDato, $scope.newCampo.opciones,
                    $scope.newCampo.obligatorio).then(function (d) {
                        $scope.validsolierr = d.data;
                        cont++;
                        if ($scope.validsolierr.errorCode == '0') {
                            $scope.validerror = true;
                        } else {
                            $scope.validerror = false;
                            $scope.validmsgerror = soli.tipoSolicitud + " " + $scope.validsolierr.errorMessage;
                        }
                        if (cont == $scope.newSolicitud.length) {
                            if ($scope.validerror) {
                                $scope.newCampo = {};
                                for (let x of $scope.tiposSolicitud.ListTipoSolicitud) {
                                    x.checked = false;
                                }
                                $scope.closeModal('myModalNuevoCampo');
                                $scope.getCampos();
                            }
                            else {
                                $scope.newCampo = {};
                                $scope.closeModal('myModalNuevoCampo');
                                $scope.getCampos();
                                alert($scope.validmsgerror);
                            }
                        }
                    });
            }
        } else {
            if (!$scope.newCampo.producto) {
                $('#data-campo').addClass('alert alert-danger');
                $('#data-campo').text('Debe diligenciar este campo');
            }
            if ($scope.newSolicitud.length == 0) {
                $('#data-campo1').addClass('alert alert-danger');
                $('#data-campo1').text('Debe diligenciar este campo');
            }
        }
    }
    $scope.getIdCampo = function (codigo_campo) {
        BayportService.getIdCampo(codigo_campo).then(function (d) {
            $scope.Campo = d.data;
            if ($scope.Campo.msg.errorCode != '200') {
                alert($scope.Campo.msg.errorMessage);
            }
            else {
                $scope.Campo = $scope.Campo.ListCampos[0];
                if ($scope.Campo.solicitud) {
                    $scope.Campo.solicitud = Number($scope.Campo.solicitud);
                }
                if ($scope.Campo.dependencia) {
                    $scope.Campo.dependencia = Number($scope.Campo.dependencia);
                    $scope.getProductoDependencia($scope.Campo.dependencia);
                }
                if ($scope.Campo.producto) {
                    $scope.Campo.producto = Number($scope.Campo.producto);
                }
                if ($scope.Campo.periodo) {
                    $scope.Campo.periodo = Number($scope.Campo.periodo);
                }
                setTimeout(function () {
                    $scope.openModal('myModalEditCampo');
                }, 5000)
            }
        }, function (err) {
            alert('Error al consultar el Campo Parámterizado. ', err);
        });
    }
    $scope.updCampo = function (cod_campo, campo, tipo_dato, opciones, obligatorio) {
        console.log(tipo_dato)
        BayportService.updCampo(cod_campo, campo, tipo_dato, opciones, obligatorio).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Campo = {};
                $scope.closeModal('myModalEditCampo');
                $scope.getCampos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteCampo = function (cod_campo) {
        BayportService.deleteCampo(cod_campo).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.closeModal('myModalDeleteCampo');
                $scope.objEliminar = {};
                $scope.getCampos();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    /* Documentos */
    $scope.getDocumentos = function (dependencia, producto) {
        BayportService.getDocumentos(dependencia, producto).then(function (d) {
            $scope.documentos = d.data;
        });
    }
    $scope.getDocumentosConfig = function (dependencia, producto) {
        BayportService.getDocumentosConfig(dependencia, producto).then(function (d) {
            $scope.documentos = d.data;
        });
    }
    $scope.documentoSelect = function (filtro) {
        console.log($scope.documentos)
        $scope.docSelect = $scope.documentos.ListDocumentos.filter(d => d.cod_documento === filtro);
        console.log($scope.docSelect)
    }
    $scope.getIdDocumento = function (codigo) {
        BayportService.getIdDocumento(codigo).then(function (d) {
            $scope.Documento = d.data;
            if ($scope.Documento.msg.errorCode != '200') {
                alert($scope.Documento.msg.errorMessage);
            }
            else {
                $scope.Documento = $scope.Documento.ListDocumentos[0];
                if ($scope.Documento.dependencia) {
                    $scope.Documento.dependencia = Number($scope.Documento.dependencia);
                    $scope.getProductoDependencia($scope.Documento.dependencia);
                }
                if ($scope.Documento.producto) {
                    $scope.Documento.producto = Number($scope.Documento.producto);
                }
                $scope.Documento.firma = String($scope.Documento.firma);
                $scope.Documento.llena_auto = String($scope.Documento.llena_auto);
                $scope.Documento.expedientillo = String($scope.Documento.expedientillo);
                $scope.Documento.compra = String($scope.Documento.compra);
                $scope.openModal('myModalEdit');
            }
        }, function (err) {
            alert('Error al realizar la consulta. ', err);
        });
    }
    $scope.saveDocumento = function () {
        if ($scope.newDocumento) {
            if ($scope.newDocumento.nombre && $scope.newDocumento.orden && $scope.newDocumento.dependencia) {
                setTimeout(function () {
                    if ($scope.productos.ListProductosDependencia.length > 0) {
                        if (!$scope.newDocumento.producto) alert('Debe asociar un producto');
                        else {
                            $scope.saveDocumentoConf();
                        }
                    }
                    else {
                        $scope.saveDocumentoConf();
                    }
                }, 500);

            }
        }
    }
    $scope.saveDocumentoConf = function () {
        if (document.getElementById('plantillaDoc').files[0]) {
            $scope.newDocumento.nombreDoc = $scope.nombreArchivo;
            $scope.newDocumento.firma = Number($scope.newDocumento.firma);
            $scope.newDocumento.llena_auto = Number($scope.newDocumento.llena_auto)
            $scope.newDocumento.expedientillo = Number($scope.newDocumento.expedientillo)
            $scope.newDocumento.compra = Number($scope.newDocumento.compra)
            var fdata = new FormData();
            fdata.append("Documento", JSON.stringify($scope.newDocumento));
            var archivo = document.getElementById('plantillaDoc').files[0];
            fdata.append("plantilla", archivo);
            BayportService.saveDocumentos(fdata).then(function (d) {
                if (d.data.msg.errorCode == '0') {
                    $scope.newDocumento = {};
                    $scope.closeModal('myModalNueva');
                    $scope.nombreArchivo = "";
                    $('#data-plantillaDoc').text("");
                    document.getElementById('plantillaDoc').value = '';
                    $scope.getDocumentos($scope.DocumentoLoad.dependencia, $scope.DocumentoLoad.producto);
                } else {
                    alert(d.data.msg.errorMessage);
                }
            });
        }
        else {
            alert('Debe asociar un documento');
            $("#requerido").addClass('alert alert-danger');
            $('#requerido').text('No se pueden repetir números');
        }
    }
    $scope.updDocumento = function () {
        var fdata = new FormData();
        //$scope.Documento.file = null;
        fdata.append("Documento", JSON.stringify($scope.Documento));
        var archivo = document.getElementById('plantillaDocUpd').files[0];
        fdata.append("plantilla", archivo);
        
            BayportService.updDocumentos(fdata).then(function (d) {
                if (d.data.errorCode == '0') {
                    document.getElementById('plantillaDocUpd').value = '';
                    $scope.Documento = {};
                    $scope.closeModal('myModalEdit');
                    $('#data-plantillaDocUpd').text('');
                    $scope.getDocumentos($scope.DocumentoLoad.dependencia, $scope.DocumentoLoad.producto);
                } else {
                    alert(d.data.errorMessage);
                }
            });
    }
    $scope.deleteDocumento = function (codigo) {
        BayportService.deleteDocumentos(codigo).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getDocumentos($scope.DocumentoLoad.dependencia, $scope.DocumentoLoad.producto);
            } else {
                alert(d.data.errorMessage);
            }
        }, function (err) {
            alert('Error Eliminando este Documento. ', err);
        });
    }
    /*Paises*/
    $scope.getPaises = function () {
        BayportService.getPais().then(function (d) {
            $scope.Paises = d.data;
        });
    }
    $scope.getIdPais = function (codigo) {
        BayportService.getIdPais(codigo).then(function (d) {
            if (d.data.msg.errorCode == '200') {
                $scope.Pais = d.data.ListPais[0];
                $scope.openModal('myModalEdit');
            } else {
                alert(d.data.msg.errorMessage);
            }
        }, function (err) {
            alert('Error consultando el País.', err);
        });
    }
    $scope.savePais = function () {
        if ($scope.newPais) {
            console.log($scope.newPais);
            if ($scope.newPais.codigo && $scope.newPais.codigoPais && $scope.newPais.nombre) {
                BayportService.savePais($scope.newPais.codigo, $scope.newPais.nombre, $scope.newPais.codigoPais).then(function (d) {
                    if (d.data.errorCode == '0') {
                        $scope.newPais = {};
                        $scope.closeModal('myModalNueva');
                        $scope.getPaises();
                    } else {
                        alert(d.data.errorMessage);
                    }
                }, function (err) {
                    alert('Error guardando el país', err);
                });
            }
        }
    }
    $scope.updPais = function (id_pais, nombre_pais, codigo_pais) {
        BayportService.updPais(id_pais, nombre_pais, codigo_pais).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Pais = {};
                $scope.closeModal('myModalEdit');
                $scope.getPaises();

            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deletePais = function (codigo) {
        BayportService.deletePais(codigo).then(function (d) {
            if (d.data.msg.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getPaises();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    /*Entidades Federativas*/
    $scope.getEntidadFederativa = function () {
        BayportService.getEntidadesFederativas().then(function (d) {
            $scope.Entidades = d.data;
        }, function (err) {
            alert('Error consultando Entidades Federativas. ', err);
        });
    }
    $scope.getIdEntidadFederativa = function (codigo) {
        BayportService.getIdEntidad(codigo).then(function (d) {
            if (d.data.msg.errorCode == '200') {
                $scope.EntidadFederativa = d.data.ListEntidades[0];
                if ($scope.EntidadFederativa.codigo_pais) {
                    $scope.EntidadFederativa.codigo_pais = Number($scope.EntidadFederativa.codigo_pais);
                }
                $scope.openModal('myModalEdit');
            } else {
                alert(d.data.msg.errorMessage);
            }
        }, function (err) {
            alert('Error al consultar la Enitdad Federativa. ', err);
        });
    }
    $scope.saveEntidadFederativa = function () {
        if ($scope.NewEntidadFed) {
            if ($scope.NewEntidadFed.pais && $scope.NewEntidadFed.codigo && $scope.NewEntidadFed.nombre) {
                BayportService.saveEntidad($scope.NewEntidadFed.codigo, $scope.NewEntidadFed.nombre, $scope.NewEntidadFed.pais, $scope.NewEntidadFed.abreviatura).then(function (d) {
                    if (d.data.errorCode == '0') {
                        $scope.NewEntidadFed = {};
                        $scope.closeModal('myModalNueva');
                        $scope.getEntidadFederativa();
                    } else {
                        alert(d.data.errorMessage);
                    }
                }, function (err) {
                    alert('Error guardando la Entidad Federativa', err);
                });
            }
        }
    }
    $scope.updEntidadFederativa = function (codigo, nombre, abrev) {
        BayportService.updEntidad(codigo, nombre, abrev).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.EntidadFederativa = {};
                $scope.closeModal('myModalEdit');
                $scope.getEntidadFederativa();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteEntidadFederativa = function (codigo) {
        BayportService.deleteEntidadFederativa(codigo).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete')
                $scope.getEntidadFederativa();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    /*Municipios*/
    $scope.saveMunicipio = function () {
        if ($scope.newMunicipio) {
            if ($scope.newMunicipio.codigo_municipio && $scope.newMunicipio.nombre_municipio && $scope.newMunicipio.entidad_federativa && $scope.newMunicipio.pais) {
                BayportService.saveMunicipio($scope.newMunicipio.codigo_municipio, $scope.newMunicipio.nombre_municipio, $scope.newMunicipio.entidad_federativa, $scope.newMunicipio.pais).then(function (d) {
                    if (d.data.errorCode == '0') {
                        $scope.newMunicipio = {};
                        $scope.closeModal('myModalNueva');
                        $scope.getMunicipios();
                    } else {
                        alert(d.data.errorMessage)
                    }
                }, function (err) {
                    alert('Error al guardar el Municipio. ', err)
                });
            }
        }
    }
    $scope.updMunicipio = function (codigo, municipio, entidad, pais) {
        BayportService.updMunicipio(codigo, municipio, pais, entidad).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Municipio = {};
                $scope.closeModal('myModalEdit');
                $scope.getMunicipios();
            } else {
                alert(d.data.errorMessage);
            }
        }, function (err) {
            alert('Error al actulizar el Municipio. ', err);
        });
    }
    $scope.deleteMunicipio = function (codigo, pais, entidad) {
        BayportService.deleteMunicipio(codigo, pais, entidad).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDeleteM');
                $scope.getMunicipios();
            } else {
                alert(d.data.errorMessage);
            }
        }, function (err) {
            alert('Error al Eliminar el municipio. ', err)
        });
    }
    $scope.getMunicipios = function () {
        BayportService.getMunicipios().then(function (d) {
            if (d.data.msg.errorCode == '200') {
                $scope.Municipios = d.data;
            }
        }, function (err) {
            alert('Error Consultando los Municipios. ', err);
        });
    }
    $scope.getIdMunicipio = function (codigo, pais, entidad) {
        BayportService.getIdMunicipio(codigo, pais, entidad).then(function (d) {
            if (d.data.msg.errorCode == '200') {
                $scope.Municipio = d.data.ListMunicipios[0];
                if ($scope.Municipio.pais) {
                    $scope.Municipio.pais = Number($scope.Municipio.pais);
                }
                if ($scope.Municipio.entidad_federativa) {
                    $scope.Municipio.entidad_federativa = Number($scope.Municipio.entidad_federativa);
                }
                $scope.openModal('myModalEdit');
            }
        }, function (err) {
            alert('Error consultando el Municipio', err);
        });
    }
    /* Configuración de Documentos */
    $scope.newConfiguracion = {};
    $scope.newConfiguracion.validacion = false;
    $scope.cambiaValidacion = function (validacion) {
        if (!validacion) {
            $scope.newConfiguracion.campoValidar = "";
            $scope.newConfiguracion.vlrComprara = "";
            $scope.Configuracion.campoValidar = "";
            $scope.Configuracion.valor_validacion = "";
        }
    }
    $scope.saveconfiguracionDoc = function () {
        console.log($scope.newConfiguracion)
        if ($scope.newConfiguracion) {
            var valida = 'N';
            valida = ($scope.newConfiguracion.validacion) ? 'S' : 'N';
            if ($scope.newConfiguracion.documento && $scope.newConfiguracion.ObtDato && $scope.newConfiguracion.px && $scope.newConfiguracion.py && $scope.newConfiguracion.dato && $scope.newConfiguracion.pagina && $scope.newConfiguracion.fuente)
                BayportService.saveConfigDoc($scope.newConfiguracion.documento, $scope.newConfiguracion.ObtDato, $scope.newConfiguracion.px, $scope.newConfiguracion.py,
                    $scope.newConfiguracion.dato, $scope.newConfiguracion.pagina, $scope.newConfiguracion.fuente, valida, $scope.newConfiguracion.campoValidar, $scope.newConfiguracion.vlrComprara, $scope.newConfiguracion.aumentoy).then(function (d) {
                        if (d.data.errorCode == '0') {
                            $scope.newConfiguracion = {};
                            $scope.docSelect = null;
                            $scope.closeModal('myModalNueva');
                            $scope.getConfigDocumentos($scope.DocumentoLoad.dependencia, $scope.DocumentoLoad.producto, $scope.DocumentoLoad.documento);
                        } else {
                            alert(d.data.errorMessage)
                        }
                    }, function (err) {
                        alert('Error al guardar la configuiración. ', err);
                    });
        }
    }
    $scope.updConfigDoc = function () {
        var valida = ($scope.Configuracion.tvalidacion) ? 'S' : 'N';
        if ($scope.Configuracion.cod_documento && $scope.Configuracion.tipo_optencion && $scope.Configuracion.posicion_x && $scope.Configuracion.posicion_y && $scope.Configuracion.valor && $scope.Configuracion.pagina && $scope.Configuracion.fuente)
            BayportService.updConfigDoc($scope.Configuracion.codigo_config, $scope.Configuracion.cod_documento, $scope.Configuracion.tipo_optencion, $scope.Configuracion.posicion_x,
                $scope.Configuracion.posicion_y, $scope.Configuracion.valor, $scope.Configuracion.pagina, $scope.Configuracion.fuente, valida, $scope.Configuracion.campoValidar, $scope.Configuracion.valor_validacion, $scope.Configuracion.aumentoy).then(function (d) {
                    if (d.data.errorCode == '0') {
                        $scope.Configuracion = {};
                        $scope.docSelect = null;
                        $scope.closeModal('myModalEdit');
                        $scope.getConfigDocumentos($scope.DocumentoLoad.dependencia, $scope.DocumentoLoad.producto, $scope.DocumentoLoad.documento);
                    } else {
                        alert(d.data.errorMessage)
                    }
                }, function (err) {
                    alert('Error consultabdo las configuraciones. ', err)
                });
    }
    $scope.deleteConfigDoc = function (codigo) {
        BayportService.deleteConfigDoc(codigo).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getConfigDocumentos($scope.DocumentoLoad.dependencia, $scope.DocumentoLoad.producto, $scope.DocumentoLoad.documento);
            } else {
                alert(d.data.errorMessage);
            }
        }, function (err) {
            alert('Error al Eliminar el municipio. ', err)
        });
    }
    $scope.campoValidarAnterior = '';
    $scope.getIdConfigDocumentos = function (codigo_config) {
        BayportService.getIdConfigDocumentos(codigo_config).then(function (d) {
            if (d.data.msg.errorCode != '200') {
                alert(d.data.msg.errorMessage);
            } else {
                $scope.Configuracion = d.data.ListConfiguracion[0];
                //$scope.getDocumentosConfig($scope.Configuracion.dependencia, $scope.Configuracion.producto);
                $scope.Configuracion.tvalidacion = ($scope.Configuracion.tvalidacion == 'S') ? true : false;
                $scope.opcionesConfigurar($scope.Configuracion.campoValidar);
                if ($scope.Configuracion && $scope.Configuracion.cod_documento) {
                    $scope.documentoSelect($scope.Configuracion.cod_documento);
                }
                $scope.openModal('myModalEdit');
                /*setTimeout(function () {
                    $scope.openModal('myModalEdit');
                }, 1000)*/
            }
            //$scope.ConfiguracionDoc = d.data;
        }, function (err) {
            alert('Error al consultar la configuración. ', err);
        });
    }
    $scope.getConfigDocumentos = function (dependencia, producto, documento) {
        if (dependencia) {
            BayportService.getConfigDocumentos(dependencia, producto, documento).then(function (d) {
                $scope.ConfiguracionDocs = d.data;
                for (let i of $scope.ConfiguracionDocs.ListConfiguracion) {
                    i.optencion = (i.tipo_optencion == '1') ? 'VALOR PREDETERMINADO' : 'VALOR FORMULARIO';
                }
                console.log($scope.ConfiguracionDocs.ListConfiguracion)
            }, function (err) {
                alert('Error consultado las Configuraciones. ', err);
            });
        }
    }
    $scope.verPDF = function () {
        BayportService.docConfig($scope.DocumentoLoad.dependencia, $scope.DocumentoLoad.producto, $scope.DocumentoLoad.documento).then(function (d) {
            d.data = (d.data.indexOf('http://originacionbayport') > -1) ? d.data.replace('http', 'https') : d.data;
            $window.open(d.data, '_blank', '');
        });
    }
    $scope.DownloadDocFiles = function (url) {
        BayportService.DownloadDocFiles(url).then(function (d) {
            console.log('DownloadDocFiles ' + d.data);
            d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') : d.data.virtualpath;
            $window.open(d.data.virtualpath, '_blank', '');
        }, function (error) {
            alert('Error GetDocumentsxAsesorxState!');
        });
    };
    $scope.CamposFormulario = function () {
        BayportService.camposFormulario().then(function (d) {
            $scope.CamposFormulario = d.data;
        });
    }
    //Bancos
    $scope.getBancos = function () {
        BayportService.getBanco().then(function (d) {
            $scope.Bancos = d.data;
        });
    }
    $scope.getIdBanco = function (codigo) {
        BayportService.getIdBanco(codigo).then(function (d) {
            if (d.data.msg.errorCode == '200') {
                $scope.Banco = d.data.ListBancos[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    $scope.saveBanco = function () {
        if ($scope.newBanco.codigo_banco && $scope.newBanco.nombre_banco) {
            BayportService.saveBanco($scope.newBanco.codigo_banco, $scope.newBanco.nombre_banco).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.newBanco = {};
                    $scope.closeModal('myModalNueva');
                    $scope.getBancos();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe diligenciar todo el formulario');
        }
    }
    $scope.updBanco = function (cod, nombre) {
        BayportService.updBanco(cod, nombre).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.Bancos = {};
                $scope.closeModal('myModalEdit');
                $scope.getBancos();
            } else {
                alert('Error ', d.data.errorMessage);
            }
        });
    }
    $scope.deleteBanco = function (cod) {
        BayportService.deleteBanco(cod).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getBancos();
            }
            else {
                alert('Error ' + d.data.errorMessage);
            }
        });
    }

    //Carga Parámteros desde CSV
    $scope.openDoc = function () {
        $window.open($scope.Documento.path, '_blank', '');
    }
    $scope.sendParams = false;
    $scope.fileCSV = [];
    $scope.rows = null;
    $scope.uploadedFile = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            var boton = element.id;
            $scope.nombreArchivo = element.files["0"].name;
            var nombreLabel = "#data-" + boton;
            $(nombreLabel).text($scope.nombreArchivo);
        });
    }
    $scope.readCSV = function () {
        var filename = document.getElementById("loadParametros");
        if (filename.value.length < 1) {
            alert("Por favor cargue un archivo");
        } else {
            var file = filename.files[0];
            console.log(file)
            if (filename.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    var table = $("<table />").addClass('table table-hover');

                    $scope.rows = e.target.result.trim().split("\n");
                    var cell = null;
                    for (var i = 0; i < $scope.rows.length; i++) {
                        var row = $("<tr  />");
                        var cells = $scope.rows[i].split(";");
                        for (var j = 0; j < cells.length; j++) {
                            if (j == 0) {
                                cell = $("<th />").css('border', '1px solid');
                                cell.html(cells[j]);
                                row.append(cell);
                            } else {
                                cell = $("<td />").css('border', '1px solid');
                                cell.html(cells[j]);
                                row.append(cell);
                            }
                        }
                        table.append(row);
                    }
                    $("#dvCSV").html('');
                    $("#dvCSV").append(table);
                }
                reader.readAsText(filename.files[0]);
                $scope.sendParams = true;
            }
        }
    }
    $scope.descartaArchivo = function () {
        $("#dvCSV").html('');
        $scope.rows = [];
        $('#data-loadParametros').text('');
        document.getElementById("loadParametros").value = '';
        $scope.sendParams = false;
    }
    $scope.uploadParams = function (valid) {
        if (!$scope.Parametro.parametro) {
            valid = false;
            $('#parametroReq').addClass('alert alert-danger');
            $('#parametroReq').text('Debe diligenciar los campos requeridos*');
        }
        if (valid) {
            $scope.erroresCarga = [];
            var error = {};
            switch ($scope.Parametro.parametro) {
                case '0':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveDependencia(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '1':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveDescuento(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '2':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveDestinoCredito(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '3':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveEmpresaTelefonica(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '4':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveEstadoCivil(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '5':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveIngresos(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '6':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveMediosDisposicion(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '7':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.savePeriodos(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '8':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        var rex = new RegExp(/^(?=.*[0-9])(?=.*\d)?([0-9]\,?)*[0-9]$/);
                        var coincide = {};
                        coincide = rex.exec(fila[1].trim());
                        var repetidos = 0;
                        if (coincide) {
                            repetidos = $scope.findrepetido(fila[1].split(','));
                            if (repetidos == 0) {
                                BayportService.savePlazoCSV(fila[0], fila[1]).then(function (d) {
                                    if (d.data.errorCode != 0) {
                                        error = {
                                            "valor": d.data.errorMessage.split(".")[1],
                                            "Error": d.data.errorMessage.split(".")[0]
                                        }
                                        $scope.erroresCarga = [...$scope.erroresCarga, error];
                                    }
                                });
                            }
                            else {
                                error = {
                                    "valor": fila[1],
                                    "Error": 'No pueden haber valores repetidos.'
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        } else {
                            error = {
                                "valor": fila[1],
                                "Error": 'El valor no esta correcto. Cada número debe estar separado por , y el valor debe terminar en un número'
                            }
                            $scope.erroresCarga = [...$scope.erroresCarga, error];
                        }
                    }
                    break;
                case '9':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveSector(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '10':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        BayportService.savePuestoCSV(fila[0], fila[1]).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '11':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveReca(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '12':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        BayportService.saveSucursales(fila[0], fila[1], fila[2], fila[3], fila[4], fila[5]).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '13':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveTipoSolicitud(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '14':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveTipoNomina(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '15':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i]
                        BayportService.saveTiposIdentificacion(fila).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '16':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        BayportService.saveProductoCSV(fila[0], fila[1]).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '17':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        BayportService.savePais(fila[0], fila[1], fila[2]).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '18':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        BayportService.saveEntidad(fila[1], fila[2], fila[0]).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '19':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        BayportService.saveMunicipio(fila[2], fila[3], fila[1], fila[0]).then(function (d) {
                            if (d.data.errorCode != 0) {
                                error = {
                                    "valor": d.data.errorMessage.split(".")[1],
                                    "Error": d.data.errorMessage.split(".")[0]
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        });
                    }
                    break;
                case '20':
                    for (var i = 1; i < $scope.rows.length; i++) {
                        var fila = $scope.rows[i].split(';');
                        fila[5] = fila[5].toUpperCase();
                        fila[7] = fila[7].toUpperCase();
                        var rex = new RegExp(/^(?=.*[0-9a-zA-Z])(?=.*\d)?([A-Za-z0-9]\,?)*[0-9A-Za-z]$/);
                        fila[0] = (fila[0] == '') ? ' ' : fila[0];
                        fila[2] = (fila[2] == '') ? ' ' : fila[2];
                        fila[3] = (fila[3] == '') ? ' ' : fila[3];
                        if (fila[5] == "O" && fila[6]) {
                            var coincide = rex.exec(fila[6].trim());
                            if (coincide) {
                                BayportService.saveCamposCSV(fila[0], fila[1], fila[2], fila[3], fila[4], fila[5], fila[6], fila[7]).then(function (d) {
                                    if (d.data.errorCode != 0) {
                                        error = {
                                            "valor": d.data.errorMessage.split(".")[1],
                                            "Error": d.data.errorMessage.split(".")[0]
                                        }
                                        $scope.erroresCarga = [...$scope.erroresCarga, error];
                                    }
                                });
                            } else {
                                error = {
                                    "valor": fila[6],
                                    "Error": 'El valor no esta correcto. Cada número debe estar separado por , y el valor debe terminar en un número'
                                }
                                $scope.erroresCarga = [...$scope.erroresCarga, error];
                            }
                        } else {
                            BayportService.saveCamposCSV(fila[0], fila[1], fila[2], fila[3], fila[4], fila[5], fila[6], fila[7]).then(function (d) {
                                console.log(d.data, '-->', d.data.errorCode != 0)
                                if (d.data.errorCode != 0) {
                                    error = {
                                        "valor": d.data.errorMessage.split(".")[1],
                                        "Error": d.data.errorMessage.split(".")[0]
                                    }
                                    $scope.erroresCarga = [...$scope.erroresCarga, error];
                                }
                            });
                        }
                    }
                    break;
            }
            setTimeout(function () {
                if ($scope.erroresCarga.length > 0) {
                    $scope.openModal('ErroresCargar');
                } else {
                    $("#dvCSV").html('');
                    $scope.rows = [];
                    $scope.sendParams = false;
                    alert('Los parámteros se han cargado exitosamente');
                }
            }, 10000)
        }
    }
    $scope.getDivision = function () {
        BayportService.getDivision().then(function (d) {
            $scope.divisiones = d.data;
        }, function (err) {
            alert('Se ha presentado un error. ', err);
        });
    }
    $scope.getRegionDivision = function (division) {
        BayportService.getRegionDivision(division).then(function (d) {
            $scope.Regiones = d.data;
        }, function (err) {
            alert('Se ha presentado un error. ', err);
        });
    }
    $scope.getTipoSucursal = function () {
        BayportService.getTipoSucursal().then(function (d) {
            $scope.TipoSucursales = d.data;
        }, function (err) {
            alert('Se ha presentado un error. ', err);
        });
    }
    $scope.uploadedFilePlantilla = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            
            var boton = element.id;
            var prueba = "#" + boton;
            $scope.nombreArchivo = element.files["0"].name;
            var nombreLabel = "#data-plantillaDoc";
            $(nombreLabel).text($scope.nombreArchivo);
            var reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.file64 = e.target.result.split("base64,")[1];
                });
            }
            reader.readAsDataURL($scope.file);
            var value = element.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {

                //$scope.file64 = e.target.result.split("base64,")[1];
                $scope.newDocumento.file = e.target.result.split("base64,")[1];
                var fileCom = atob($scope.newDocumento.file);
                switch ($scope.file.type) {
                    case 'application/pdf':
                        if (fileCom.indexOf('%PDF-1.') === -1) {
                            $scope.newDocumento.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(boton).value = '';
                            $scope.nombreArchivo = '';
                            return;
                        }
                        break;
                    case 'image/png':
                        if (fileCom.indexOf('PNG') === -1) {
                            $scope.newDocumento.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(boton).value = '';
                            $scope.nombreArchivo = '';
                            return;
                        }
                        break;
                    case 'image/jpeg':
                        if (fileCom.indexOf('JFIF') > -1 && ($scope.file.name.indexOf('.jpg') === -1 && $scope.file.name.indexOf('.jpeg') === -1)) {
                            $scope.newDocumento.file = null; 
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(boton).value = '';
                            $scope.nombreArchivo = '';
                            return;
                        }
                        break;
                    default:
                        $scope.newDocumento.file = null;
                        alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                        $(nombreLabel).text('');
                        document.getElementById(boton).value = '';
                        console.log(document.getElementById(boton))
                        $scope.nombreArchivo = '';
                        return;
                        break;
                }
                $scope.newDocumento.file = null;
                $scope.newDocumento.nombreDoc = $scope.nombreArchivo;
            }
            reader.readAsDataURL(value);
        });
    }
    $scope.UpduploadedFilePlantilla = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            var boton = element.id;
            var prueba = "#" + boton;
            $scope.nombreArchivo = element.files["0"].name;
            var nombreLabel = '#data-plantillaDocUpd';
            $(nombreLabel).text($scope.nombreArchivo);
            var reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.file64 = e.target.result.split("base64,")[1];
                });
            }
            reader.readAsDataURL($scope.file);
            var value = element.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                //$scope.file64 = e.target.result.split("base64,")[1];
                $scope.Documento.file = e.target.result.split("base64,")[1];
                var fileCom = atob($scope.Documento.file);
                switch ($scope.file.type) {
                    case 'application/pdf':
                        if (fileCom.indexOf('%PDF-1.') === -1) {
                            $scope.Documento.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(boton).value = '';
                            return;
                        }
                        break;
                    case 'image/png':
                        if (fileCom.indexOf('PNG') === -1) {
                            $scope.Documento.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(boton).value = '';
                            return;
                        }
                        break;
                    case 'image/jpeg':
                        if (fileCom.indexOf('JFIF') > -1 && ($scope.file.name.indexOf('.jpg') === -1 && $scope.file.name.indexOf('.jpeg') === -1)) {
                            $scope.Documento.file = null;
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(boton).value = '';
                            return;
                        }
                        break;
                    default:
                        $scope.Documento.file = null;
                        alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                        $(nombreLabel).text('');
                        document.getElementById(boton).value = '';
                        return;
                        break;
                }
                $scope.Documento.file = null;
                $scope.Documento.nombreDoc = $scope.nombreArchivo;
            }
            reader.readAsDataURL(value);
        });
    }
    $scope.newAvisos = {}
    $scope.newAvisos["Imagenes"] = [];
    $scope.uploadedFileSAvisos = function (element) {
        $scope.$apply(function ($scope) {
            console.log(element.files)
            var boton = element.id;
            for (let f of element.files) {
                var error = false;
                var prueba = "#" + boton;
                var nombreDocActual = f.name;
                var reader = new FileReader();
                reader.onload = function (e) {
                    $scope.$apply(function () {
                        $scope.file64 = e.target.result.split("base64,")[1];
                    });
                }
                reader.readAsDataURL(f);
                var value = f;
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$scope.file64 = e.target.result.split("base64,")[1];
                    var file = e.target.result.split("base64,")[1];
                    var file64 = e.target.result
                    var fileCom = atob(file);
                    switch (f.type) {
                        case 'image/png':
                            if (fileCom.indexOf('PNG') === -1) {
                                error = true;
                                break;
                            }
                            break;
                        case 'image/jpeg':
                            if (fileCom.indexOf('JFIF') > -1 && (f.name.indexOf('.jpg') === -1 && f.name.indexOf('.jpeg') === -1)) {
                                error = true;
                                break;
                            }
                            break;
                        default:
                            error = true;
                            alert('Los Archivos Cargados no son validos. Por favor cargue un Archivo JPG o PNG ');
                            return;
                            break;
                    }
                    var data = {
                        'file': ((error)?'': f),
                        'imageDataUrl': file64,
                        'name': nombreDocActual,
                        'error': error
                    }
                    $scope.newAvisos["Imagenes"] = [...$scope.newAvisos["Imagenes"], data]
                    console.log($scope.newAvisos["Imagenes"])
                }
                reader.readAsDataURL(value);
            }
            
        });
    }
    $scope.listadoOpciones = [];
    $scope.opcionesConfigurar = function (campo) {
        $scope.listadoOpciones = [];
        var data = null;
        switch (campo) {
            case 'TIPO_SOLICITUD':
                BayportService.getTipoSolicitud().then(function (d) {
                    data = d.data;
                    for (let i of data.ListTipoSolicitud) {
                        var obj = { value: i.tipoSolicitud, label: i.tipoSolicitud };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'PERIODO':
                BayportService.getPeriodos().then(function (d) {
                    data = d.data;
                    for (let i of data.ListPeriodos) {
                        var obj = { value: i.periodo, label: i.periodo };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'DEPENDENCIA':
                BayportService.getDependencias().then(function (d) {
                    data = d.data;
                    for (let i of data.ListDependencias) {
                        var obj = { value: i.dependencia, label: i.dependencia };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'PRODUCTO':
                BayportService.getProductos().then(function (d) {
                    data = d.data;
                    for (let i of data.ListProductos) {
                        var obj = { value: i.producto, label: i.producto };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'DESTINO':
                BayportService.getDestino().then(function (d) {
                    data = d.data;
                    for (let i of data.ListDestinoCred) {
                        var obj = { value: i.destinoCredito, label: i.destinoCredito };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'QUINCENA_DSCTO':
                BayportService.getDescuentos().then(function (d) {
                    data = d.data;
                    for (let i of data.ListQuincenaDscto) {
                        var obj = { value: i.QuincenaDscto, label: i.QuincenaDscto };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'IDENTIFICACION':
                BayportService.getTiposIdentificacion().then(function (d) {
                    data = d.data;
                    for (let i of data.ListTipoIdentificacion) {
                        var obj = { value: i.tipoIdentificacion, label: i.tipoIdentificacion };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'ESTADO_CIVIL':
                BayportService.getEstadoCivil().then(function (d) {
                    data = d.data;
                    for (let i of data.ListEstados) {
                        var obj = { value: i.estadoCivil, label: i.estadoCivil };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'GENERO':
                $scope.listadoOpciones.push({ value: 'M', label: 'Masculino' })
                $scope.listadoOpciones.push({ value: 'F', label: 'Femenino' })
                break;
            case 'SECTOR':
                BayportService.getSector().then(function (d) {
                    data = d.data;
                    for (let i of data.ListSector) {
                        var obj = { value: i.sector, label: i.sector };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'PUESTO':
                BayportService.getPuestos().then(function (d) {
                    data = d.data;
                    for (let i of data.ListPuestos) {
                        var obj = { value: i.puestoSector, label: i.puestoSector };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'INGRESOS':
                BayportService.getIngresos().then(function (d) {
                    data = d.data;
                    for (let i of data.ListIngresos) {
                        var obj = { value: i.ingresoNeto, label: i.ingresoNeto };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'CARGO_PUBLICO':
                $scope.listadoOpciones.push({ value: 'S', label: 'Si' })
                $scope.listadoOpciones.push({ value: 'N', label: 'No' })
                break;
            case 'CARGO_PUBLICO_FAM':
                $scope.listadoOpciones.push({ value: 'S', label: 'Si' })
                $scope.listadoOpciones.push({ value: 'N', label: 'No' })
                break;
            case 'BENEFICIARIO':
                $scope.listadoOpciones.push({ value: 'S', label: 'S' })
                $scope.listadoOpciones.push({ value: 'N', label: 'N' })
                break;
            case 'EMP_TEL':
                BayportService.getEmpresaTelefonica().then(function (d) {
                    data = d.data;
                    for (let i of data.ListEmpresas) {
                        var obj = { value: i.empresa, label: i.empresa };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'MED_DISP':
                BayportService.getMediosDisposicion().then(function (d) {
                    data = d.data;
                    for (let i of data.ListMedios) {
                        var obj = { value: i.medioDisposicion, label: i.medioDisposicion };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'MED_DISP_ALT1':
                BayportService.getMediosDisposicion().then(function (d) {
                    data = d.data;
                    for (let i of data.ListMedios) {
                        var obj = { value: i.medioDisposicion, label: i.medioDisposicion };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'MED_DISP_ALT2':
                BayportService.getMediosDisposicion().then(function (d) {
                    data = d.data;
                    for (let i of data.ListMedios) {
                        var obj = { value: i.medioDisposicion, label: i.medioDisposicion };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'BANCO':
                BayportService.getBanco().then(function (d) {
                    data = d.data;
                    for (let i of data.ListBancos) {
                        var obj = { value: i.nombre_banco, label: i.nombre_banco };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'BANCO_ALT1':
                BayportService.getBanco().then(function (d) {
                    data = d.data;
                    for (let i of data.ListBancos) {
                        var obj = { value: i.nombre_banco, label: i.nombre_banco };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'BANCO_ALT2':
                BayportService.getBanco().then(function (d) {
                    data = d.data;
                    for (let i of data.ListBancos) {
                        var obj = { value: i.nombre_banco, label: i.nombre_banco };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
                break;
            case 'PAIS_NACIMIENTO':
                $scope.listadoOpciones.push({ value: 'MÉXICO', label: 'MÉXICO' })
                $scope.listadoOpciones.push({ value: 'OTRO', label: 'OTRO' })
                break;
            case 'PAIS_RESIDENCIA':
                $scope.listadoOpciones.push({ value: 'MÉXICO', label: 'MÉXICO' })
                $scope.listadoOpciones.push({ value: 'OTRO', label: 'OTRO' })
                break;
            case 'NACIONALIDAD':
                $scope.listadoOpciones.push({ value: 'MEXICANO', label: 'MEXICANO' })
                $scope.listadoOpciones.push({ value: 'OTRA', label: 'OTRA' })
                break;
            case 'RECA':
                BayportService.getReca().then(function (d) {
                    data = d.data;
                    for (let i of data.ListRecas) {
                        var obj = { value: i.reca, label: i.reca };
                        $scope.listadoOpciones = [...$scope.listadoOpciones, obj];
                    }
                    $scope.Configuracion.valor_validacion = $scope.Configuracion.valor_validacion
                });
        }
    }
    $scope.validaEmailCoor = function (email) {
        if (email) {
            if (email.toUpperCase().indexOf('@BAYPORT') == -1) {
                $scope.newSucursal.emailCoordinador = "";
                $scope.Sucursal.emailCoordinador = "";
                alert('El email del Coordinador debe ser corporativo');
            }
        }
    }
    $scope.validaEmailAux = function (email) {
        if (email) {
            if (email.toUpperCase().indexOf('@BAYPORT') == -1) {
                $scope.newSucursal.emailAuxiliar = "";
                $scope.Sucursal.emailAuxiliar = "";
                alert('El email del Auxuliar debe ser corporativo');
            }
        }
    }
    //Casas Financieras

    $scope.getCasasFinancierasActivas = function () {
        BayportService.getCasasFinancierasActivas().then(function (d) {
            $scope.casasFinancierasList = d.data;
        });
    }
    $scope.saveCasaFinanciera = function () {
        if ($scope.newDependencia && $scope.newDependencia.codigo && $scope.newDependencia.nombre) {
            BayportService.saveDependencia($scope.newDependencia.codigo, $scope.newDependencia.nombre, $scope.newDependencia.estado).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.newDependencia = {};
                    $scope.closeModal('myModalNueva');
                    $scope.getCasaFinanciera();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe diligencias todo los campos');
        }
    }
    //Casas Financieras
    $scope.getCasasFinancierasActivas = function () {
        BayportService.getcasasFinancierasActivas().then(function (d) {
            $scope.casasFinancierasList = d.data;
        });
    }
    $scope.saveCasaFinanciera = function () {
        if ($scope.newCasaFinanciera && $scope.newCasaFinanciera.codigo && $scope.newCasaFinanciera.nombre) {
            BayportService.saveCasaFinanciera($scope.newCasaFinanciera.codigo, $scope.newCasaFinanciera.nombre, $scope.newCasaFinanciera.estado).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.newCasaFinanciera = {};
                    $scope.closeModal('myModalNueva');
                    $scope.getCasaFinanciera();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert('Debe diligencias todo los campos');
        }
    }
    $scope.getCasasFinanciera = function () {
        BayportService.getCasaFinanciera().then(function (d) {
            $scope.casasFinancieras = d.data;
        });
    }
    $scope.updCasaFinanciera = function (id, nombre, estado) {
        BayportService.updCasaFinanciera(id, nombre, estado).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.getCasasFinanciera();
                $scope.closeModal('myModalEdit');
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteCasasFinanciera = function (id) {
        BayportService.deleteCasasFinanciera(id).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getCasaFinanciera();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.getIdCasaFinanciera = function (id) {
        BayportService.getIdCasaFinanciera(id).then(function (d) {
            $scope.casaFinanciera = d.data;
            if ($scope.casaFinanciera.msg.errorCode != '200') {
                alert($scope.casaFinanciera.msg.errorMessage);
            }
            else {
                $scope.casaFinanciera = $scope.casaFinanciera.ListCasas[0];
                $scope.openModal('myModalEdit');
            }
        });
    }
    /* CLAVE PRESUPUESTAL*/
    $scope.getClaves = function () {
        BayportService.getClaves().then(function (d) {
            $scope.ClavesDelgList = d.data;
        });
    }
    $scope.getIdClave = function (cod) {
        BayportService.getIdClave(cod).then(function (d) {     
            if (d.data.msg.errorCode != "0") {
                alert(d.data.errorMessage);
            } else {
                $scope.claveDelg = d.data.ListClavesDelg[0];
                $scope.openModal("myModalEdit");
            }
        });
    }
    $scope.saveClaveDelg = function () {
        if ($scope.newclaveDelg && $scope.newclaveDelg.clave && $scope.newclaveDelg.delegacion) {
            BayportService.saveClaveDelg($scope.newclaveDelg.clave, $scope.newclaveDelg.delegacion).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.newclaveDelg = {};
                    $scope.closeModal('myModalNueva');
                    $scope.getClaves();
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            alert("Todos los campos son obligatorios")
        }
    }
    $scope.updClaveDelg = function (cod, nombre) {
        BayportService.updClaveDelg(cod, nombre).then(function (d) {
            if (d.data.errorCode == "0") {
                $scope.closeModal("myModalEdit");
                $scope.getClaves();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
    $scope.deleteClave = function (cod) {
        BayportService.deleteClave(cod).then(function (d) {
            if (d.data.errorCode == "0") {
                $scope.objEliminar = {};
                $scope.closeModal('myModalDelete');
                $scope.getClaves();
            } else {
                alert(d.data.errorMessage);
            }
        });
    }
});

app.controller('OriginacionController', function ($scope, BayportService, $filter, $location, $window) {
    $scope.UserOptions1 = null;
    $scope.UserOptions2 = null;
    $scope.UserOptions3 = null;
    $scope.UserOptions4 = null;
    $scope.UserOptions5 = null;
    $scope.UserOptions6 = null;
    $scope.UserOptions7 = null;
    $scope.UserOptions8 = null;
    $scope.UserOptions9 = null;
    $scope.UserOptions10 = null;
    $scope.regexHoraContacto = /^(?=.*[0-9])(?=.*\d)?([0-9]\:?)*[0-9]$/;
    $scope.GetUserOptions = function (i) {
        BayportService.GetUserOptions(i).then(function (d) {
            switch (i) {
                case 1:
                    $scope.UserOptions1 = d.data;
                    break;
                case 2:
                    $scope.UserOptions2 = d.data;
                    break;
                case 3:
                    $scope.UserOptions3 = d.data;
                    break;
                case 4:
                    $scope.UserOptions4 = d.data;
                    break;
                case 5:
                    $scope.UserOptions5 = d.data;
                    break;
                case 6:
                    $scope.UserOptions6 = d.data;
                    break;
                case 7:
                    $scope.UserOptions7 = d.data;
                    break;
                case 8:
                    $scope.UserOptions8 = d.data;
                    break;
                case 9:
                    $scope.UserOptions9 = d.data;
                    break;
                case 10:
                    $scope.UserOptions10 = d.data;
                    break;
                default:
                    break;
            }
        }, function (error) {
            //alert('Error GetUserOptions!');
        });
    };
    $scope.folderCancelar = {};
    $scope.cancelarFolder = function (item) {
        $scope.folderCancelar = item;
    }
    $scope.cancelarFolio = function (folder) {
        BayportService.cancelarFolio(folder).then(function (d) {
            if (d.data.errorCode == '0') {
                $scope.getListadoSolicitudes();
            } else {
                alert(d.data.errorMessage);
            }
            $scope.closeModal('modalConfirmacion');
        });
    }
    $scope.expedienteCompletoLoad = "0";

    $scope.checked = false;
    $scope.ofertaaceptada = {};
    $scope.formulario = {};
    $scope.formularioAlterno = {};
    $scope.formulario.estado = '';
    $scope.formulario.cartera = [];
    $scope.formulario.expediente_completo = 0;
    $scope.formulario.cliente_siebel = 0;
    $scope.fchsolicitud = new Date();
    $scope.NoSpecialCharactersNombres = /^[A-Za-z'ñÑáéíóú/-\s]+$/; //'^[a-zA-Z0-9]+$';
    $scope.NoSpecialCharacters = /^[A-Za-zñÑáéíóú\s]+$/; //'^[a-zA-Z0-9]+$';
    $scope.NoSpecialCharactersRfc = /^[A-Za-zñÑ0-9\s]+$/
    $scope.NoSpecialCharactersCodigo = /^[0-9\s]+$/
    $scope.ExpclavePresupuestal = /^[A-Z.a-zñÑ0-9/-\\s]+$/;
    $scope.regexEmail = /^[^@]+@[^@]+\.[a-zA-Z]{2,}$/
    $scope.procesarPerfil = "Enviar al Auxiliar Administrativo";
    $scope.dependenciaList = null;
    $scope.tipoSolicitudList = null;
    $scope.periodosAplicables = null;
    $scope.plazos = null;
    $scope.plazosList = [];
    $scope.nominaList = null;
    $scope.cartera = [];
    $scope.campoAdicional = false;
    $scope.opcionesAdicionales = [];
    $scope.enviarSolicitud = false;
    $scope.listColonias = [];
    $scope.listColoniasOcupacion = [];
    $scope.originacionDoc = {
        fila: "",
        nombreDoc: "",
        folder: 0,
        codigo_doc: 0,
        dependencia: 0,
        producto: 0,
        fima: 1,
    };
    $scope.docPoliza = function (carpeta) {
        BayportService.docPoliza(carpeta).then(function (d) {
            if (d.data.virtualpath != "" && d.data.virtualpath != null) {
                console.log('DownloadDocFiles ' + d.data.virtualpath);
                d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') : d.data.virtualpath;

                $window.open(d.data.virtualpath, '_blank', '');
            }
        });
    }
    $scope.validaEmail = function (email) {
        console.log(email)
        $("#valid-domicilio").text('');
        $("#valid-domicilio").removeClass('alert alert-danger');
        if (email.toUpperCase().indexOf('@BAYPORT') > -1) {
            $scope.formulario.emailContacto = '';
            $("#valid-domicilio").text('El correo no debe ser corporativo');
            $("#valid-domicilio").addClass('alert alert-danger');
        }
    }
    $scope.analizaCurp = function (curp) {
        var valid = $scope.cadenaValida(curp, 18);
        if (valid) {
            var date = new Date();
            $scope.formulario.gender = (curp.substring(10, 11) == 'M') ? 'F' : 'M';
            var d = curp.substring(8, 10);
            var m = curp.substring(6, 8);
            var y = curp.substring(4, 6);
            var yactual = date.getFullYear();
            var yact = yactual + "";
            yact = yact.substring(1, 3);
            if (Number(y) < Number(yact)) {
                y = "20" + y;
            } else {
                y = "19" + y;
            }
            $scope.formulario.fecNac = d + '/' + m + '/' + y;
            if (curp.substring(11, 13) != 'NE') {
                $scope.formulario.paisN = 260;
                $scope.formulario.paisR = 260;
                $scope.formulario.nacionalidad = 'MEXICANO';
                $scope.getEntidadesFedN($scope.formulario.paisN);
                BayportService.entidadAbrev(curp.substring(11, 13)).then(function (d) {
                    $scope.formulario.entidadN = d.data;

                });
                $scope.getEntidadesFedR($scope.formulario.paisR)
            } else {
                $scope.formulario.paisN = null;
                $scope.formulario.paisR = null;
                $scope.formulario.entidadN = null;
            }
        }
    }
    $scope.analizaCodigoPostal = function (codigo) {
        $scope.listColonias = [];
        BayportService.codigoPostal(codigo).then(function (d) {
            console.log(d.data);
            for (let item of d.data.colonias) {
                item = item.toUpperCase();
                var obj = { label: item, values: item };
                $scope.listColonias = [...$scope.listColonias, obj];
            }

            $scope.formulario.entidadDom = d.data.estado
            $scope.formulario.municipioDom = d.data.municipio;
        });
    }
    $scope.analizaCodigoPostalOcupacion = function (codigo) {
        $scope.listColoniasOcupacion = [];
        BayportService.codigoPostal(codigo).then(function (d) {
            if (d.data.estatus == '000') {
                for (let item of d.data.colonias) {
                    item = item.toUpperCase();
                    var obj = { label: item, values: item };
                    $scope.listColoniasOcupacion = [...$scope.listColoniasOcupacion, obj];
                }
                $scope.formulario.entidadT = d.data.estado;
                $scope.formulario.municipio = d.data.municipio;
            } else {
                var error = 'Ocupación ' + d.data.descripcionMovimiento;
                alert(error);
            }
        });
    }
    $scope.validaAntiguedad = function (fecha) {
        var date = new Date();
        var y = date.getFullYear();
        var m = date.getMonth();
        var y1 = fecha.substring(6, 10);
        var m1 = fecha.substring(3, 5);
        $scope.formulario.antiguedad = Number(y) - Number(y1);
        if (Number(m) < Number(m1)) {
            $scope.formulario.antiguedad = $scope.formulario.antiguedad - 1;
        }
    }
    $scope.PrecargarPerfilamiento = function () {
        $scope.getClaves();
        $scope.getTipoSolicitud();
        $scope.GetDependencias();
        $scope.getPeriodos();
        $scope.getNomina();
        $scope.getDestino();
        $scope.getDescuentos();
        $scope.getSucursal();
        $scope.getIdentificacion();
        $scope.getSector();
        $scope.getIngresos();
        $scope.getmedioDispo();
        $scope.getPaises();
        $scope.EmpresasTelefonicas();
        $scope.getBancos();
        $scope.getEstadoCivil();
        $scope.listParentesco();
        $scope.getCasasFinancierasActivas();
        $scope.existeFormulario();


    }
    $scope.existeFormulario = function () {
        if ($window.localStorage["formulario"] && $window.localStorage["formulario"] != null) {

            $scope.formulario = JSON.parse($window.localStorage["formulario"]);
            $scope.formularioAlterno = JSON.parse($window.localStorage["formulario"]);
            $scope.formulario.plazo = String($scope.formulario.plazo);
            $scope.fchsolicitud = $scope.formulario.fchsolicitud.substring(0, 10);
            $scope.formulario.fchsolicitud = $scope.formulario.fchsolicitud.substring(0, 10);
            $scope.formulario.fchPrPago = $scope.formulario.fchPrPago.substring(0, 10);
            $scope.formulario.fchUltPago = $scope.formulario.fchUltPago.substring(0, 10);
            $scope.formulario.fecNac = $scope.formulario.fecNac.substring(0, 10);
            $scope.formulario.fchIngreso = $scope.formulario.fchIngreso.substring(0, 10);
            $scope.formulario.telFijo = Number($scope.formulario.telFijo);
            $scope.formulario.CelularContacto = Number($scope.formulario.CelularContacto);
            $scope.formulario.telefonoPropio = Number($scope.formulario.telefonoPropio);
            $scope.formulario.TelefonoRef1 = Number($scope.formulario.TelefonoRef1);
            $scope.formulario.TelefonoRef2 = Number($scope.formulario.TelefonoRef2);
            $scope.formulario.CelularRef1 = Number($scope.formulario.CelularRef1);
            $scope.formulario.CelularRef2 = Number($scope.formulario.CelularRef2);
            $scope.formulario.CompanyPhone = Number($scope.formulario.CompanyPhone);
            //$scope.formulario.nss = Number($scope.formulario.nss);
            $scope.formulario.matricula1 = Number($scope.formulario.matricula1);
            $scope.formulario.matricula2 = Number($scope.formulario.matricula2);
            $scope.formulario.Pagaduria = Number($scope.formulario.Pagaduria);
            $scope.formulario.grupo = Number($scope.formulario.grupo);
            $scope.formulario.clave_trabajdor = String($scope.formulario.clave_trabajdor);
            $scope.formulario.DiasPagar = Number($scope.formulario.DiasPagar);
            $scope.formulario.depositoCliente = Number($scope.formulario.depositoCliente);
            $scope.formulario.Hora1Ref1 = ($scope.formulario.Hora1Ref1) ? new Date(($scope.formulario.Hora1Ref1)) : "";
            $scope.formulario.Hora2Ref1 = ($scope.formulario.Hora1Ref1) ? new Date(($scope.formulario.Hora2Ref1)) : "";
            $scope.formulario.Hora1Ref2 = ($scope.formulario.Hora1Ref2) ? new Date(($scope.formulario.Hora1Ref2)) : "";
            $scope.formulario.Hora2Ref2 = ($scope.formulario.Hora1Ref2) ? new Date(($scope.formulario.Hora2Ref2)) : "";
            //Paramteros
            $scope.formulario.sector = ($scope.formulario.sector == 0) ? null : $scope.formulario.sector;
            $scope.formulario.periodo = ($scope.formulario.periodo == 0) ? null : $scope.formulario.periodo;
            $scope.formulario.Dependencia = ($scope.formulario.Dependencia == 0) ? null : $scope.formulario.Dependencia;
            $scope.formulario.paisN = ($scope.formulario.paisN == 0) ? null : $scope.formulario.paisN;
            $scope.formulario.paisR = ($scope.formulario.paisR == 0) ? null : $scope.formulario.paisR;
            $scope.formulario.entidadT = ($scope.formulario.entidadT == 0) ? null : $scope.formulario.entidadT;
            $scope.formulario.entidadDom = ($scope.formulario.entidadDom == 0) ? null : $scope.formulario.entidadDom;
            $scope.formulario.puesto = ($scope.formulario.puesto == 0) ? null : $scope.formulario.puesto;
            $scope.formulario.medioDisp = ($scope.formulario.medioDisp == 0) ? null : $scope.formulario.medioDisp;
            $scope.formulario.medioDispAlt = ($scope.formulario.medioDispAlt == 0) ? null : $scope.formulario.medioDispAlt;
            $scope.formulario.yearResidencia = Number($scope.formulario.yearResidencia)
            $scope.formulario.reca = ($scope.formulario.reca == 0) ? null : $scope.formulario.reca;
            //formularioAlterno
            $scope.formularioAlterno.plazo = String($scope.formulario.plazo);
            $scope.formularioAlterno.fchsolicitud = $scope.formulario.fchsolicitud.substring(0, 10);
            $scope.formularioAlterno.fchPrPago = $scope.formulario.fchPrPago.substring(0, 10);
            $scope.formularioAlterno.fchUltPago = $scope.formulario.fchUltPago.substring(0, 10);
            $scope.formularioAlterno.fecNac = $scope.formulario.fecNac.substring(0, 10);
            $scope.formularioAlterno.fchIngreso = $scope.formulario.fchIngreso.substring(0, 10);
            $scope.formularioAlterno.telFijo = Number($scope.formulario.telFijo);
            $scope.formularioAlterno.CelularContacto = Number($scope.formulario.CelularContacto);
            $scope.formularioAlterno.telefonoPropio = Number($scope.formulario.telefonoPropio);
            $scope.formularioAlterno.TelefonoRef1 = Number($scope.formulario.TelefonoRef1);
            $scope.formularioAlterno.TelefonoRef2 = Number($scope.formulario.TelefonoRef2);
            $scope.formularioAlterno.CelularRef1 = Number($scope.formulario.CelularRef1);
            $scope.formularioAlterno.CelularRef2 = Number($scope.formulario.CelularRef2);
            $scope.formularioAlterno.CompanyPhone = Number($scope.formulario.CompanyPhone);
            //$scope.formularioAlterno.nss = Number($scope.formulario.nss);
            $scope.formularioAlterno.matricula1 = Number($scope.formulario.matricula1);
            $scope.formularioAlterno.matricula2 = Number($scope.formulario.matricula2);
            $scope.formularioAlterno.Pagaduria = Number($scope.formulario.Pagaduria);
            $scope.formularioAlterno.grupo = Number($scope.formulario.grupo);
            $scope.formularioAlterno.clave_trabajdor = String($scope.formulario.clave_trabajdor);
            $scope.formularioAlterno.DiasPagar = Number($scope.formulario.DiasPagar);
            $scope.formularioAlterno.depositoCliente = Number($scope.formulario.depositoCliente);
            $scope.formularioAlterno.Hora1Ref1 = ($scope.formularioAlterno.Hora1Ref1) ? new Date(($scope.formularioAlterno.Hora1Ref1)) : "";
            $scope.formularioAlterno.Hora2Ref1 = ($scope.formularioAlterno.Hora1Ref1) ? new Date(($scope.formularioAlterno.Hora2Ref1)) : "";
            $scope.formularioAlterno.Hora1Ref2 = ($scope.formularioAlterno.Hora1Ref2) ? new Date(($scope.formularioAlterno.Hora1Ref2)) : "";
            $scope.formularioAlterno.Hora2Ref2 = ($scope.formularioAlterno.Hora1Ref2) ? new Date(($scope.formularioAlterno.Hora2Ref2)) : "";
            //Paramteros
            $scope.formularioAlterno.sector = ($scope.formularioAlterno.sector == -1) ? null : $scope.formularioAlterno.sector;
            $scope.formularioAlterno.periodo = ($scope.formularioAlterno.periodo == -1) ? null : $scope.formularioAlterno.periodo;
            $scope.formularioAlterno.Dependencia = ($scope.formularioAlterno.Dependencia == -1) ? null : $scope.formularioAlterno.Dependencia;
            $scope.formularioAlterno.paisN = ($scope.formularioAlterno.paisN == -1) ? null : $scope.formularioAlterno.paisN;
            $scope.formularioAlterno.paisR = ($scope.formularioAlterno.paisR == -1) ? null : $scope.formularioAlterno.paisR;
            $scope.formularioAlterno.entidadT = ($scope.formularioAlterno.entidadT == -1) ? null : $scope.formularioAlterno.entidadT;
            $scope.formularioAlterno.entidadDom = ($scope.formularioAlterno.entidadDom == -1) ? null : $scope.formularioAlterno.entidadDom;
            $scope.formularioAlterno.puesto = ($scope.formularioAlterno.puesto == -1) ? null : $scope.formularioAlterno.puesto;
            $scope.formularioAlterno.medioDisp = ($scope.formularioAlterno.medioDisp == -1) ? null : $scope.formularioAlterno.medioDisp;
            $scope.formularioAlterno.medioDispAlt = ($scope.formularioAlterno.medioDispAlt == -1) ? null : $scope.formularioAlterno.medioDispAlt;
            $scope.formularioAlterno.yearResidencia = Number($scope.formularioAlterno.yearResidencia)
            $scope.formularioAlterno.reca = ($scope.formularioAlterno.reca == -1) ? null : $scope.formularioAlterno.reca;
            $scope.selecCliente = ($scope.formulario.cliente_siebel == 0) ? false : true;
            //funciones
            $scope.getPlazosPeriodo($scope.formulario.periodo);
            if ($scope.formulario.tipoDoc != 0) $scope.identificacionOficial($scope.formulario.tipoDoc);
            $scope.getProductoDependencia($scope.formulario.Dependencia);
            if ($scope.formulario.codPostDom) {
                $scope.analizaCodigoPostal($scope.formulario.codPostDom);
            }
            if ($scope.formulario.CodigoPost) {
                $scope.analizaCodigoPostalOcupacion($scope.formulario.CodigoPost);
            }
            setTimeout(function () {
                $scope.validaCampos($scope.formulario.tipoSolicitud, $scope.formulario.Dependencia, $scope.formulario.periodo);
            }, 500);
            setTimeout(function () {
                $scope.loadDocumentos();
            }, 500);
            if ($scope.formulario.sector) $scope.getPuestoSector($scope.formulario.sector);
            if ($scope.formulario.paisN) $scope.getEntidadesFedN($scope.formulario.paisN);
            if ($scope.formulario.paisR) $scope.getEntidadesFedR($scope.formulario.paisR);
            if ($scope.formulario.paisR && $scope.formulario.entidadT) $scope.municipioR($scope.formulario.paisR, $scope.formulario.entidadT);
            if ($scope.formulario.paisR && $scope.formulario.entidadDom) $scope.municipioDom($scope.formulario.paisR, $scope.formulario.entidadDom);
            $scope.expedienteCompletoLoad = $scope.formulario.expediente_completo.toString();
            if ($scope.formulario.expediente_completo === 1) {
                $scope.findExpCompleto($scope.formulario.folderNumber);
            }
            if ($scope.formulario.RFC) {
                $scope.rfc_ant = $scope.formulario.RFC;
            }
            $("#solicitud1").attr('data-toggle', 'tab');
            $("#datos1").attr('data-toggle', 'tab');

            if ($scope.formulario.sector) {
                $("#ocupacion1").attr('data-toggle', 'tab');
            }
            if ($scope.formulario.codPostDom) {
                $("#domicilio1").attr('data-toggle', 'tab');
            }
            if ($scope.formulario.nombreRef1) {
                $("#referencias1").attr('data-toggle', 'tab');
            }
            setTimeout(function () {
                console.log('habilita pestañas')
                if ($scope.formulario.medioDisp) {
                    $("#medios1").attr('data-toggle', 'tab');
                    if (!$scope.compra) {
                        $("#documentos1").attr('data-toggle', 'tab');
                        if ($scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == 'Devolucion Auxiliar' || $scope.formulario.estado == 'Devolucion') {
                            $scope.enviarSolicitud = true;
                        }
                    }
                }
                if ($scope.cartera) {
                    $("#cartera1").attr('data-toggle', 'tab');
                    if ($scope.compra) {
                        $("#documentos1").attr('data-toggle', 'tab');
                        if ($scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == 'Devolucion Auxiliar' || $scope.formulario.estado == 'Devolucion') {
                            $scope.enviarSolicitud = true;
                        }
                    }
                }
            }, 7000)
        } else {
            $scope.sucursalAsesor();
        }
    }

    $scope.findExpCompleto = function (folder) {
        BayportService.findExpCompleto(folder).then(function (d) {
            $scope.DocumentosCargados = d.data.ListDocumentos;
        });
    }
    $scope.getIdentificacion = function () {
        BayportService.getTiposIdentificacion().then(function (d) {
            $scope.identificacionList = d.data;
        });
    }
    $scope.getClaves = function () {
        BayportService.getClaves().then(function (d) {
            $scope.ClavesDelgList = d.data;
        });
    }
    $scope.claveDelegacion = function (cod) {
        $scope.delegacionExist = $scope.ClavesDelgList.ListClavesDelg.filter(d => d.clave === cod);
        console.log($scope.delegacionExist);
        $scope.formulario.delegacionImss = $scope.delegacionExist[0].delegacion;
    }
    $scope.getNomina = function () {
        BayportService.getTipoNomina().then(function (d) {
            $scope.nominaList = d.data;
        });
    }
    $scope.GetDependencias = function () {
        BayportService.getDependenciasActivas().then(function (d) {
            $scope.dependenciaList = d.data;
            $scope.DependenciaoTextToShow = "SELECCIONE DEPENDENCIA";
        });
    }
    $scope.getTipoSolicitud = function () {
        BayportService.getTipoSolicitud().then(function (d) {
            $scope.tipoSolicitudList = d.data;
            if ($scope.formulario.tipoSolicitud) {
                $scope.ChangeSolicitud($scope.formulario.tipoSolicitud);
            }
        });
    }
    $scope.getPeriodos = function () {
        BayportService.getPeriodos().then(function (d) {
            $scope.periodosAplicables = d.data;
        });
    }
    $scope.getPlazosPeriodo = function (periodo) {
        $scope.plazosList = [];
        BayportService.getPlazosPeriodo(periodo).then(function (d) {
            $scope.plazos = d.data;
            $scope.Arrayplazos = $scope.plazos.ListPlazoPeriodo[0].plazos.split(",");
            for (var i = 0; i < $scope.Arrayplazos.length; i++) {
                $scope.plazosList = [...$scope.plazosList, {
                    value: $scope.Arrayplazos[i],
                    plazo: $scope.Arrayplazos[i]
                }];
            }
        });
    }
    $scope.getDestino = function () {
        BayportService.getDestino().then(function (d) {
            $scope.destinoList = d.data;
        });
    }
    $scope.getDescuentos = function () {
        BayportService.getDescuentos().then(function (d) {
            $scope.descuentosList = d.data;
        });
    }
    $scope.getSucursal = function () {
        BayportService.getSucursales().then(function (d) {
            $scope.SucursalesList = d.data;
        });
    }
    $scope.getProductoDependencia = function (dependencia) {
        BayportService.getProductosDependencia(dependencia).then(function (d) {
            $scope.ProductosList = d.data;
        },
            function (err) {
                alert(err);
            })
    }
    $scope.getSector = function () {
        BayportService.getSector().then(function (d) {
            $scope.sectoresList = d.data;
            if ($scope.formulario.sector && $scope.formulario.sector != 0) {
                $scope.validaSector($scope.formulario.sector)
            }
        });
    }
    $scope.getPuestoSector = function (sector) {
        BayportService.getPuestoSector(sector).then(function (d) {
            $scope.puestosList = d.data;
        });
    }
    $scope.getIngresos = function () {
        BayportService.getIngresos().then(function (d) {
            $scope.IngresosList = d.data;
        });
    }
    $scope.getmedioDispo = function () {
        BayportService.getMediosDisposicion().then(function (d) {
            $scope.mediosList = d.data;
            if ($scope.formulario.medioDisp && $scope.formulario.medioDisp != 0) $scope.medioDisposicion($scope.formulario.medioDisp);

        });
    }
    $scope.addCompraCartera = function () {
        if ($scope.compraDeduda.fechaContrato && $scope.compraDeduda.casaFinanciera && $scope.compraDeduda.capital
            && $scope.compraDeduda.saldo && $scope.compraDeduda.dsctoCompra && $scope.compraDeduda.plazoCompra
            && $scope.compraDeduda.saldoInsolutoCompra && $scope.compraDeduda.tasaCompra) {

            var compraCartera = {
                fecha: $scope.compraDeduda.fechaContrato,
                entidad: $scope.compraDeduda.casaFinanciera,
                capital: $scope.compraDeduda.capital,
                totPagar: $scope.compraDeduda.saldo,
                descuento: $scope.compraDeduda.dsctoCompra,
                plazo: $scope.compraDeduda.plazoCompra,
                saldoInsoluto: $scope.compraDeduda.saldoInsolutoCompra,
                tasa: $scope.compraDeduda.tasaCompra,
            }
            $scope.formulario.cartera = [...$scope.formulario.cartera, compraCartera];
            $scope.compraDeduda = {};
        } else {
            alert('Todos los campos son requeridos');
        }
    }
    $scope.sucursalAsesor = function () {
        BayportService.getSucursalAsesor().then(function (d) {
            $scope.formulario.sucursal = d.data.sucursal;
        });
    }
    $scope.getReca = function () {
        BayportService.getReca().then(function (d) {
            $scope.recaList = d.data;
        });
    }
    $scope.getEstadoCivil = function () {
        BayportService.getEstadoCivil().then(function (d) {
            $scope.EstadoCivilList = d.data;
        });
    }
    $scope.removeItem = function (item) {
        var index = $scope.formulario.cartera.indexOf(item);
        if (item.item) {
            BayportService.eliminarItemCartera(item.item, $scope.formulario.folderNumber).then(function (d) {
                if (d.data.errorCode == '0') {
                    $scope.formulario.cartera.splice(index, 1);
                } else {
                    alert(d.data.errorMessage);
                }
            });
        } else {
            $scope.formulario.cartera.splice(index, 1);
        }
    }
    $scope.nextTab = function (actual, option) {
        for (var i = 1; i <= 31; i++) {
            label = '#valid-' + actual + i;
            $(label).removeClass('alert alert-danger');
            $(label).text('');
        }
        $scope.valid = true;
        var ntab = '#' + option
        var tabAc = '#' + actual
        var label = '';

        switch (actual) {
            case 'solicitud':
                if ($scope.fchsolicitud && !$scope.formulario.fchsolicitud) {
                    var d = $scope.fchsolicitud.getDate();
                    var m = $scope.fchsolicitud.getMonth() + 1;
                    var y = $scope.fchsolicitud.getFullYear();
                    $scope.formulario.fchsolicitud = (d < 10) ? '0' + d + '/' : d + '/';
                    $scope.formulario.fchsolicitud += (m < 10) ? '0' + m + '/' : m + '/';
                    $scope.formulario.fchsolicitud += y;
                }
                if (!$scope.formulario.tipoSolicitud) {
                    $('#valid-' + actual + '2').addClass('alert alert-danger');
                    $('#valid-' + actual + '2').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.monto) {
                    $('#valid-' + actual + '3').addClass('alert alert-danger');
                    $('#valid-' + actual + '3').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.periodo) {
                    $('#valid-' + actual + '4').addClass('alert alert-danger');
                    $('#valid-' + actual + '4').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.plazo) {
                    $('#valid-' + actual + '5').addClass('alert alert-danger');
                    $('#valid-' + actual + '5').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.LBase) {
                    $('#valid-' + actual + '6').addClass('alert alert-danger');
                    $('#valid-' + actual + '6').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.formulario.Lbase) {
                    if ($scope.formulario.Lbase < 0) {
                        $('#valid-solicitud6').addClass('text-danger');
                        $('#valid-solicitud6').text('El valor debe ser mayor a 0');
                        $scope.valid = false;
                    }
                }
                if (!$scope.formulario.Dependencia) {
                    $('#valid-' + actual + '7').addClass('alert alert-danger');
                    $('#valid-' + actual + '7').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.ProductosList.ListProductosDependencia.length > 0) {
                    if (!$scope.formulario.producto) {
                        $('#valid-' + actual + '8').addClass('alert alert-danger');
                        $('#valid-' + actual + '8').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if (!$scope.formulario.destino) {
                    $('#valid-' + actual + '9').addClass('alert alert-danger');
                    $('#valid-' + actual + '9').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.tNomina) {
                    $('#valid-' + actual + '10').addClass('alert alert-danger');
                    $('#valid-' + actual + '10').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.dscto) {
                    $('#valid-' + actual + '11').addClass('alert alert-danger');
                    $('#valid-' + actual + '11').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.tAnual) {
                    $('#valid-' + actual + '12').addClass('alert alert-danger');
                    $('#valid-' + actual + '12').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                console.log($scope.formulario.cat);
                if ($scope.formulario.cat == null) {
                    $('#valid-' + actual + '13').addClass('alert alert-danger');
                    $('#valid-' + actual + '13').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.sucursal) {
                    $('#valid-' + actual + '14').addClass('alert alert-danger');
                    $('#valid-' + actual + '14').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.quincenaDscto) {
                    $('#valid-' + actual + '15').addClass('alert alert-danger');
                    $('#valid-' + actual + '15').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.cPago) {
                    $('#valid-' + actual + '16').addClass('alert alert-danger');
                    $('#valid-' + actual + '16').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.formulario.cPago) {
                    if ($scope.formulario.cPago < 0) {
                        $('#valid-solicitud16').addClass('text-danger');
                        $('#valid-solicitud16').text('El valor debe ser mayor a 0');
                        $scope.valid = false;
                    }
                }
                if (!$scope.formulario.mMaxPlaz) {
                    $('#valid-' + actual + '17').addClass('alert alert-danger');
                    $('#valid-' + actual + '17').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.formulario.mMaxPlaz) {
                    if ($scope.formulario.mMaxPlaz < 0) {
                        $('#valid-solicitud17').addClass('text-danger');
                        $('#valid-solicitud17').text('El valor debe ser mayor a 0');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('ESPECIFICAR') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('ESPECIFICAR');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.especificar) {
                        $('#valid-' + actual + '18').addClass('alert alert-danger');
                        $('#valid-' + actual + '18').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('GRUPO') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('GRUPO');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.grupo) {
                        $('#valid-' + actual + '19').addClass('alert alert-danger');
                        $('#valid-' + actual + '19').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('MATRICULA') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('MATRICULA');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.matricula) {
                        $('#valid-' + actual + '20').addClass('alert alert-danger');
                        $('#valid-' + actual + '20').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('NSS') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('NSS');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.nss) {
                        $('#valid-' + actual + '21').addClass('alert alert-danger');
                        $('#valid-' + actual + '21').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    } else if ($scope.obligatorio[i] == 'S' && !$scope.cadenaValida1($scope.formulario.nss, 9)) {
                        $('#valid-' + actual + '21').addClass('text-danger');
                        $('#valid-' + actual + '21').text('Ha ingresado menos de 9 carácteres. Por Favor Revise');
                        $scope.valid = false;
                    } else if ($scope.obligatorio[i] == 'N' && !$scope.cadenaValida1($scope.formulario.nss, 9)) {
                        $('#valid-' + actual + '21').addClass('text-danger');
                        $('#valid-' + actual + '21').text('Ha ingresado menos de 9 carácteres. Por Favor Revise');
                        $scope.formulario.nss = null;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('MONTO DEUDOR') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('MONTO DEUDOR');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.monto_deudor) {
                        $('#valid-' + actual + '22').addClass('alert alert-danger');
                        $('#valid-' + actual + '22').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('CLAVE TRABAJDOR') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('CLAVE TRABAJDOR');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.clave_trabajdor) {
                        $('#valid-' + actual + '23').addClass('alert alert-danger');
                        $('#valid-' + actual + '23').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('RECA') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('RECA');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.reca) {
                        $('#valid-' + actual + '24').addClass('alert alert-danger');
                        $('#valid-' + actual + '24').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('FECHA PRIMER PAGO') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('FECHA PRIMER PAGO');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.clave_trabajdor) {
                        $('#valid-' + actual + '25').addClass('alert alert-danger');
                        $('#valid-' + actual + '25').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('FECHA PRIMER PAGO') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('FECHA PRIMER PAGO');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.clave_trabajdor) {
                        $('#valid-' + actual + '26').addClass('alert alert-danger');
                        $('#valid-' + actual + '26').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                break;
            case 'datos':
                if (!$scope.formulario.RFC) {
                    $('#valid-' + actual + '1').addClass('alert alert-danger');
                    $('#valid-' + actual + '1').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.cadenaValida($scope.formulario.RFC, 13)) {
                    $('#valid-' + actual + '1').addClass('text-danger');
                    $('#valid-' + actual + '1').text('Ha ingresado más de 13 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if (!$scope.formulario.pNombre) {
                    $('#valid-' + actual + '2').addClass('alert alert-danger');
                    $('#valid-' + actual + '2').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.pApellido && !$scope.formulario.sApellido) {
                    $('#valid-' + actual + '3').addClass('alert alert-danger');
                    $('#valid-' + actual + '3').text('Capturar o Registrar los campos requeridos');
                    $('#valid-' + actual + '4').addClass('alert alert-danger');
                    $('#valid-' + actual + '4').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.tipoDoc) {
                    $('#valid-' + actual + '5').addClass('alert alert-danger');
                    $('#valid-' + actual + '5').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.CURP) {
                    $('#valid-' + actual + '6').addClass('alert alert-danger');
                    $('#valid-' + actual + '6').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.cadenaValida($scope.formulario.CURP, 18)) {
                    $('#valid-' + actual + '6').addClass('text-danger');
                    $('#valid-' + actual + '6').text('Ha ingresado más de 18 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if (!$scope.formulario.fecNac) {
                    $('#valid-' + actual + '7').addClass('alert alert-danger');
                    $('#valid-' + actual + '7').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.paisN) {
                    $('#valid-' + actual + '8').addClass('alert alert-danger');
                    $('#valid-' + actual + '8').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.entidadN) {
                    $('#valid-' + actual + '9').addClass('alert alert-danger');
                    $('#valid-' + actual + '9').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.paisR) {
                    $('#valid-' + actual + '10').addClass('alert alert-danger');
                    $('#valid-' + actual + '10').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.tipoIdentificacion.tipoIdentificacion.toUpperCase().indexOf('OTRA') > -1 || $scope.tipoIdentificacion.tipoIdentificacion.toUpperCase().indexOf('OTRO') > -1) {

                    if (!$scope.formulario.otraIdentificacion) {
                        $('#valid-' + actual + '12').addClass('alert alert-danger');
                        $('#valid-' + actual + '12').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if (!$scope.formulario.estadoCivil) {
                    $('#valid-' + actual + '11').addClass('alert alert-danger');
                    $('#valid-' + actual + '11').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                break;
            case 'ocupacion':
                if (!$scope.formulario.sector) {
                    $('#valid-' + actual + '1').addClass('alert alert-danger');
                    $('#valid-' + actual + '1').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.puesto) {
                    $('#valid-' + actual + '2').addClass('alert alert-danger');
                    $('#valid-' + actual + '2').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.antiguedad) {
                    $('#valid-' + actual + '3').addClass('alert alert-danger');
                    $('#valid-' + actual + '3').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.ingresos) {
                    $('#valid-' + actual + '4').addClass('alert alert-danger');
                    $('#valid-' + actual + '4').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.CamposAdicionales.indexOf('NO_EMPLEADO') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('NO_EMPLEADO');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.Celular) {
                        $('#valid-' + actual + '5').addClass('alert alert-danger');
                        $('#valid-' + actual + '5').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('CLAVE_PRESUPUESTAL') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('CLAVE_PRESUPUESTAL');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.cPresupuestal) {
                        $('#valid-' + actual + '6').addClass('alert alert-danger');
                        $('#valid-' + actual + '6').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('PAGADURIA') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('PAGADURIA');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.Pagaduria) {
                        $('#valid-' + actual + '7').addClass('alert alert-danger');
                        $('#valid-' + actual + '7').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('FECHA INGRESO') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('FECHA INGRESO');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.fchIngreso) {
                        $('#valid-' + actual + '30').addClass('alert alert-danger');
                        $('#valid-' + actual + '30').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('CLAVE_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('CLAVE_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.clave) {
                        $('#valid-' + actual + '8').addClass('alert alert-danger');
                        $('#valid-' + actual + '8').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('LUGAR_TRABAJO_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('LUGAR_TRABAJO_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.lugTrabajo) {
                        $('#valid-' + actual + '9').addClass('alert alert-danger');
                        $('#valid-' + actual + '9').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }    
                }
                if ($scope.CamposAdicionales.indexOf('CALLE_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('CALLE_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.calle) {
                        $('#valid-' + actual + '10').addClass('alert alert-danger');
                        $('#valid-' + actual + '10').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('NUMERO_EXT_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('NUMERO_EXT_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.nExterior) {
                        $('#valid-' + actual + '11').addClass('alert alert-danger');
                        $('#valid-' + actual + '11').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('COLONIA_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('COLONIA_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.colonia) {
                        $('#valid-' + actual + '12').addClass('alert alert-danger');
                        $('#valid-' + actual + '12').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('TELEFONO_FIJO_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('TELEFONO_FIJO_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.telFijo) {
                        $('#valid-' + actual + '13').addClass('alert alert-danger');
                        $('#valid-' + actual + '13').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('TELEFONO_FIJO_OCP') > -1 && $scope.formulario.telFijo && !$scope.cadenaValida($scope.formulario.telFijo, 10)) {
                    $('#valid-' + actual + '13').addClass('text-danger');
                    $('#valid-' + actual + '13').text('Ha ingresado más de 10 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if ($scope.CamposAdicionales.indexOf('EXTENSION_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('EXTENSION_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.extension) {
                        $('#valid-' + actual + '31').addClass('alert alert-danger');
                        $('#valid-' + actual + '31').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('ENTIDAD_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('ENTIDAD_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.entidadT) {
                        $('#valid-' + actual + '14').addClass('alert alert-danger');
                        $('#valid-' + actual + '14').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('MUNICIPIO_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('MUNICIPIO_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.municipio) {
                        $('#valid-' + actual + '15').addClass('alert alert-danger');
                        $('#valid-' + actual + '15').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('CODIGO_POSTAL_OCP') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('CODIGO_POSTAL_OCP');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.CodigoPost) {
             
                        $('#valid-' + actual + '16').addClass('alert alert-danger');
                        $('#valid-' + actual + '16').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.formulario.tCargoPu == 'S' && !$scope.formulario.pEjecucion) {
                    if (!$scope.formulario.pEjecucion) {
                        $('#valid-' + actual + '25').addClass('alert alert-danger');
                        $('#valid-' + actual + '25').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.formulario.tCargoPuF == 'S') {
                    if (!$scope.formulario.nombFamiliar) {
                        $('#valid-' + actual + '26').addClass('alert alert-danger');
                        $('#valid-' + actual + '26').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                    if (!$scope.formulario.puestoFam) {
                        $('#valid-' + actual + '27').addClass('alert alert-danger');
                        $('#valid-' + actual + '27').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                    if (!$scope.formulario.perEjecucionFam) {
                        $('#valid-' + actual + '28').addClass('alert alert-danger');
                        $('#valid-' + actual + '28').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.formulario.tBeneneficiario == 'S' && !$scope.formulario.nombBene) {
                    if (!$scope.formulario.nombBene) {
                        $('#valid-' + actual + '29').addClass('alert alert-danger');
                        $('#valid-' + actual + '29').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('CATEGORIA O TIPO DE PENSION') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('CATEGORIA O TIPO DE PENSION');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.tipPension) {
                        $('#valid-' + actual + '17').addClass('alert alert-danger');
                        $('#valid-' + actual + '17').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('ADSCRIPCION O UBICACIÓN DEL PAGO') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('ADSCRIPCION O UBICACIÓN DEL PAGO');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.ubiPago) {
                        $('#valid-' + actual + '18').addClass('alert alert-danger');
                        $('#valid-' + actual + '18').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('DELEGACION') > -1) {
                    var i = $scope.CamposAdicionales.indexOf('DELEGACION');
                    if ($scope.obligatorio[i] == 'S' && !$scope.formulario.delegacionImss) {
                        $('#valid-' + actual + '19').addClass('alert alert-danger');
                        $('#valid-' + actual + '19').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                if ($scope.CamposAdicionales.indexOf('TESTIGOS') > -1) {
                    if (!$scope.formulario.nombTest1) {
                        $('#valid-' + actual + '20').addClass('alert alert-danger');
                        $('#valid-' + actual + '20').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                    if (!$scope.formulario.matricula1) {
                        $('#valid-' + actual + '21').addClass('alert alert-danger');
                        $('#valid-' + actual + '21').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                    if (!$scope.formulario.nombTest2) {
                        $('#valid-' + actual + '22').addClass('alert alert-danger');
                        $('#valid-' + actual + '22').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                    if (!$scope.formulario.matricula2) {
                        $('#valid-' + actual + '23').addClass('alert alert-danger');
                        $('#valid-' + actual + '23').text('Capturar o Registrar los campos requeridos');
                        $scope.valid = false;
                    }
                }
                break;
            case 'domicilio':
                if (!$scope.formulario.codPostDom) {
                    $('#valid-' + actual + '1').addClass('alert alert-danger');
                    $('#valid-' + actual + '1').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.yearResidencia) {
                    $('#valid-' + actual + '2').addClass('alert alert-danger');
                    $('#valid-' + actual + '2').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.entidadDom) {
                    $('#valid-' + actual + '3').addClass('alert alert-danger');
                    $('#valid-' + actual + '3').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.municipioDom) {
                    $('#valid-' + actual + '4').addClass('alert alert-danger');
                    $('#valid-' + actual + '4').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.coloniaDom) {
                    $('#valid-' + actual + '5').addClass('alert alert-danger');
                    $('#valid-' + actual + '5').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.domicilioCalle) {
                    $('#valid-' + actual + '6').addClass('alert alert-danger');
                    $('#valid-' + actual + '6').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.noExteriorDom) {
                    $('#valid-' + actual + '7').addClass('alert alert-danger');
                    $('#valid-' + actual + '7').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.entreCalleDom) {
                    $('#valid-' + actual + '8').addClass('alert alert-danger');
                    $('#valid-' + actual + '8').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.CelularContacto) {
                    $('#valid-' + actual + '9').addClass('alert alert-danger');
                    $('#valid-' + actual + '9').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.cadenaValida($scope.formulario.CelularContacto, 10)) {
                    $('#valid-' + actual + '9').addClass('text-danger');
                    $('#valid-' + actual + '9').text('Ha ingresado más de 10 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if (!$scope.formulario.CompanyPhone) {
                    $('#valid-' + actual + '10').addClass('alert alert-danger');
                    $('#valid-' + actual + '10').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.emailContacto) {
                    $('#valid-' + actual).addClass('alert alert-danger');
                    $('#valid-' + actual).text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.cadenaValida($scope.formulario.telefonoPropio, 10)) {
                    $('#valid-' + actual + '11').addClass('text-danger');
                    $('#valid-' + actual + '11').text('Ha ingresado más de 10 carácteres. Por favor revise.');
                    $scope.formulario.telefonoPropio = null;
                }
                break;
            case 'referencias':
                if (!$scope.formulario.nombreRef1) {
                    $('#valid-' + actual + '1').addClass('alert alert-danger');
                    $('#valid-' + actual + '1').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.pApellidoRef1 && !$scope.formulario.sApellidoRef1) {
                    $('#valid-' + actual + '2').addClass('alert alert-danger');
                    $('#valid-' + actual + '2').text('Capturar o Registrar los campos requeridos');
                    $('#valid-' + actual + '3').addClass('alert alert-danger');
                    $('#valid-' + actual + '3').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.TelefonoRef1 && !$scope.formulario.CelularRef1) {
                    $('#valid-' + actual + '4').addClass('alert alert-danger');
                    $('#valid-' + actual + '4').text('Capturar o Registrar los campos requeridos');
                    $('#valid-' + actual + '5').addClass('alert alert-danger');
                    $('#valid-' + actual + '5').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.formulario.TelefonoRef1 && !$scope.cadenaValida($scope.formulario.TelefonoRef1, 10)) {
                    $('#valid-' + actual + '4').addClass('text-danger');
                    $('#valid-' + actual + '4').text('Ha ingresado más de 10 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if ($scope.formulario.CelularRef1 && !$scope.cadenaValida($scope.formulario.CelularRef1, 10)) {
                    $('#valid-' + actual + '5').addClass('text-danger');
                    $('#valid-' + actual + '5').text('Ha ingresado más de 10 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if (!$scope.formulario.Hora1Ref1 || !$scope.formulario.Hora2Ref1 || !$scope.formulario.dia1Ref1 || !$scope.formulario.dia2Ref1) {
                    $('#valid-' + actual + '6').addClass('alert alert-danger');
                    $('#valid-' + actual + '6').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.ParentescoRef1) {
                    $('#valid-' + actual + '7').addClass('alert alert-danger');
                    $('#valid-' + actual + '7').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.nombreRef2) {
                    $('#valid-' + actual + '8').addClass('alert alert-danger');
                    $('#valid-' + actual + '8').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.pApellidoRef2 && !$scope.formulario.sApellidoRef2) {
                    $('#valid-' + actual + '9').addClass('alert alert-danger');
                    $('#valid-' + actual + '9').text('Capturar o Registrar los campos requeridos');
                    $('#valid-' + actual + '10').addClass('alert alert-danger');
                    $('#valid-' + actual + '10').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.TelefonoRef2 && !$scope.formulario.CelularRef2) {
                    $('#valid-' + actual + '11').addClass('alert alert-danger');
                    $('#valid-' + actual + '11').text('Capturar o Registrar los campos requeridos');
                    $('#valid-' + actual + '12').addClass('alert alert-danger');
                    $('#valid-' + actual + '12').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.formulario.TelefonoRef2 && !$scope.cadenaValida($scope.formulario.TelefonoRef2, 10)) {
                    $('#valid-' + actual + '11').addClass('text-danger');
                    $('#valid-' + actual + '11').text('Ha ingresado más de 10 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if ($scope.formulario.CelularRef2 && !$scope.cadenaValida($scope.formulario.CelularRef2, 10)) {
                    $('#valid-' + actual + '12').addClass('text-danger');
                    $('#valid-' + actual + '12').text('Ha ingresado más de 10 carácteres. Por favor revise.');
                    $scope.valid = false;
                }
                if (!$scope.formulario.Hora1Ref2 || !$scope.formulario.Hora2Ref2 || !$scope.formulario.dia1Ref2 || !$scope.formulario.dia2Ref2) {
                    $('#valid-' + actual + '13').addClass('alert alert-danger');
                    $('#valid-' + actual + '13').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.ParentescoRef2) {
                    $('#valid-' + actual + '14').addClass('alert alert-danger');
                    $('#valid-' + actual + '14').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                break;
            case 'medios':
                $scope.medio = $scope.mediosList.ListMedios.filter(d => d.secuencia === $scope.formulario.medioDisp);
                console.log($scope.medio);
                if (!$scope.formulario.medioDisp) {
                    $('#valid-' + actual + '1').addClass('alert alert-danger');
                    $('#valid-' + actual + '1').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.ClabeDisp && $scope.medio[0].medioDisposicion.indexOf('DEPOSITO') > -1) {
                    $('#valid-' + actual + '2').addClass('alert alert-danger');
                    $('#valid-' + actual + '2').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.NombreBanco && $scope.medio[0].medioDisposicion.indexOf('DEPOSITO') > -1) {
                    $('#valid-' + actual + '3').addClass('alert alert-danger');
                    $('#valid-' + actual + '3').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                break;
            case 'cartera':
                if (!$scope.formulario.depositoCliente) {
                    $('#valid-' + actual + '1').addClass('alert alert-danger');
                    $('#valid-' + actual + '1').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if (!$scope.formulario.DiasPagar) {
                    $('#valid-' + actual + '2').addClass('alert alert-danger');
                    $('#valid-' + actual + '2').text('Capturar o Registrar los campos requeridos');
                    $scope.valid = false;
                }
                if ($scope.formulario.cartera.length == 0) {
                    alert('Debe registrar al menos una compra de deuda');
                    $scope.valid = false;
                }
                //$scope.valid = true;
                break;
        }
        if ($scope.valid) {
            $scope.saveFormulario(actual);
            $(tabAc + '1').attr('data-toggle', 'tab')
            $(ntab + '1').attr('data-toggle', 'tab')
            $('#tabs a[href="' + ntab + '"]').tab('show');
        } else {
            alert('Capturar o Registrar los campos requeridos');
        }
    }
    $scope.getBancos = function () {
        BayportService.getBanco().then(function (d) {
            $scope.listBancos = d.data;
        });
    }
    $scope.beforeTab = function (option) {
        var tab = '#' + option;
        $('#tabs a[href="' + tab + '"]').tab('show');
    }
    $scope.OfertaPolizas = function () {
        //if (!$scope.formulario.codePlan && (!$scope.formulario.tiene_seguro || $scope.formulario.tiene_seguro == 1)) {
        BayportService.getOfertasSeguros($scope.formulario.folderNumber, $scope.formulario.monto, $scope.formulario.mMaxPlaz).then(function (d) {
            if (d.data.msg.errorCode == "0") {
                $scope.polizas = d.data.ofertas;
                if ($scope.polizas.length > 0) {
                    for (var i = 0; i < $scope.polizas.length; i++) {
                        $scope.polizas[i].vlrMensual = $scope.polizas[i].PolicyValue / $scope.polizas[i].YearsValidity;
                        $scope.polizas[i].vlrDiario = ($scope.polizas[i].PolicyValue / $scope.polizas[i].YearsValidity) / 30;
                        if ($scope.formulario.codePlan && $scope.formulario.codePlan != null || $scope.formulario.codePlan != 0) {
                            $scope.ofertaaceptada = ($scope.polizas[i].PlanCode == $scope.formulario.codePlan) ? $scope.polizas[i].PlanCode : {};
                            $scope.polizas[i].principal = ($scope.polizas[i].PlanCode == $scope.formulario.codePlan) ? "checked" : " ";
                        }
                    }
                    $("#divCheckAplica").show();
                    if ($scope.ofertaaceptada.PolicyValue) {
                        $scope.formulario.monto -= $scope.ofertaaceptada.PolicyValue;
                        $scope.formulario.codePlan = $scope.ofertaaceptada.PlanCode;
                        $scope.formulario.planValue = $scope.ofertaaceptada.PolicyValue;
                    }
                    else {
                        $scope.ofertaaceptada.observaciones = '';
                        $scope.ofertaaceptada = $scope.polizas[0];
                        $scope.polizas[0].principal = "checked";
                        $scope.formulario.monto -= $scope.ofertaaceptada.PolicyValue;
                        $scope.formulario.codePlan = $scope.ofertaaceptada.PlanCode;
                        $scope.formulario.planValue = $scope.ofertaaceptada.PolicyValue;
                    }
                    $scope.openModal('myModalOfert');
                } else {
                    $scope.msgCabecera = "Estado Consulta Poliza";
                    $scope.msgDetalle = d.data.msg.errorCode + " Este producto por el momento no ofrece Seguro";
                    $scope.openModal('myModalMensage');
                    if ($scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == 'Devolucion') {
                        $scope.enviarSolicitud = true;
                    }
                    $('#tabs a[href="#documentos"]').tab('show');
                    $scope.formulario.tiene_seguro == 0;
                }
            } else {
                $scope.msgCabecera = "Estado Consultan Poliza";
                $scope.msgDetalle = d.data.msg.errorCode + " " + d.data.msg.errorMessage;
                $scope.openModal('myModalMensage');
                if ($scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == 'Devolucion') {
                    $scope.enviarSolicitud = true;
                }
                $('#tabs a[href="#documentos"]').tab('show');
            }
        });
        /*} else {
            $scope.enviarSolicitud = true;
            $('#tabs a[href="#documentos"]').tab('show');
        }*/
    }

    $scope.validaCampos = function () {
        if ($scope.formulario.tipoSolicitud && $scope.formulario.Dependencia && $scope.formulario.periodo) {
            $scope.campoAdicional = null;
            setTimeout(function () {
                BayportService.campoAdicional($scope.formulario.tipoSolicitud, $scope.formulario.Dependencia, $scope.formulario.producto, $scope.formulario.periodo).then(function (d) {
                    $scope.campoAdicional = d.data.ListCampos;
                    $scope.CamposAdicionales = [];
                    $scope.obligatorio = [];
                    $scope.option = {};
                    console.log($scope.campoAdicional)
                    for (let item of $scope.campoAdicional) {
                        $scope.CamposAdicionales.push(item.campo);
                        $scope.obligatorio.push(item.obligatorio);
                        if (item.tipo_dato == 'O') {
                            if (item.campo != 'RECA') {
                                $scope.option[item.campo] = item.opciones.split(',');
                                console.log($scope.option);
                            } else {
                                $scope.getReca();
                            }
                        }
                    }
                });
            }, 2000);
        }
    }
    $scope.procesarFormulario = function (valid) {
        //if (valid) {
        $scope.errorSiebel = true;
        $scope.loading = true;
        $scope.procesarPerfil = "Procesando Solicitud";
        BayportService.procesarSolicitud($scope.formulario.folderNumber, $scope.formulario.Dependencia, $scope.formulario.producto).then(function (d) {
            if (d.data.errorCode == '000') {
                $window.localStorage["formulario"] = {};
                $scope.errorSiebel = false;
                $scope.TituloModal = "Detalle del Envio";
                $scope.msgCabecera = "Estatus del Envío";
                $scope.msgDetalle = d.data.errorMessage;
                $scope.openModal('myModalMensageOferta')
            } else {
                $window.localStorage["formulario"] = {};
                $scope.TituloModal = "Detalle del Envio";
                $scope.errorSiebel = true;
                $scope.msgCabecera = "Estatus del Envío";
                $scope.msgDetalle = d.data.errorMessage;
                $scope.openModal('myModalMensageOferta')
            }
        })
        //}
        //BayportService.writePDF($scope.formulario.Dependencia, $scope.formulario.producto);
    }
    $scope.redirigir = function () {
        $scope.closeModal('myModalMensageOferta')
        $window.location.href = '/Profile/Listado';
    }
    $scope.identificacionOficial = function (cod) {
        BayportService.getIdTiposIdentificacion(cod).then(function (d) {
            $scope.tipoIdentificacion = d.data.ListTipoIdentificacion[0];
        });
    }
    $scope.tamMedio = 18;
    $scope.medioDisposicion = function (cod) {
        $scope.medioDispo = $scope.mediosList.ListMedios.filter(d => d.secuencia === cod);
        if ($scope.medioDispo.length > 0) {
            if ($scope.medioDispo[0].medioDisposicion.toUpperCase().indexOf('VENTANILLA') > -1) {
                $scope.tamMedio = 13;
            } else {
                $scope.tamMedio = 18;
            }
        }
    }
    $scope.tamMedioAlt = 18;
    $scope.medioDisposicionAlt = function (cod) {
        $scope.medioDispo = $scope.mediosList.ListMedios.filter(d => d.secuencia === cod);
        if ($scope.medioDispo.length > 0) {
            if ($scope.medioDispo[0].medioDisposicion.toUpperCase().indexOf('VENTANILLA') > -1) {
                $scope.tamMedioAlt = 13;
            } else {
                $scope.tamMedioAlt = 18;
            }
        }
    }
    $scope.tamMedioAlt2 = 18;
    $scope.medioDisposicionAlt2 = function (cod) {
        $scope.medioDispo = $scope.mediosList.ListMedios.filter(d => d.secuencia === cod);
        if ($scope.medioDispo.length > 0) {
            if ($scope.medioDispo[0].medioDisposicion.toUpperCase().indexOf('VENTANILLA') > -1) {
                $scope.tamMedioAlt2 = 13;
            } else {
                $scope.tamMedioAlt2 = 18;
            }
        }
    }
    $scope.newCli = {};
    $scope.SelectCli = function (docume) {
        if ($scope.newCli.length >= 1) {
            $scope.newCli = {};
            for (let x of $scope.Clis.ListRfcs) {
                if (x.rfc != docume) {
                    x.checked = false;
                }
            }
            $scope.newCli = $filter('filter')($scope.Clis.ListRfcs, { checked: true });
        }
        else {
            $scope.newCli = $filter('filter')($scope.Clis.ListRfcs, { checked: true });
        }
    };
    $scope.closeCli = function () {
        //$scope.newCli = {};
        $scope.formulario.pNombre = "";
        $scope.formulario.sNombre = "";
        $scope.formulario.pApellido = "";
        $scope.formulario.sApellido = "";
        $scope.formulario.CURP = "";
        $scope.formulario.fecNac = "";
        $scope.formulario.gender = "";
        $scope.formulario.emailContacto = "";
        $scope.formulario.Celular = null;
        $scope.formulario.CelularContacto = "";
        $scope.formulario.codPostDom = "";
        $scope.formulario.coloniaDom = "";
        $scope.formulario.domicilioCalle = "";
        $scope.formulario.entreCalleDom = "";
        $scope.formulario.yearResidencia = null;
    };
    $scope.Clis;
    $scope.saveCli = function () {
        //if ($scope.newCli.length == 0) {
        //    $scope.formulario.pNombre = "";
        //    $scope.formulario.sNombre = "";
        //    $scope.formulario.pApellido = "";
        //    $scope.formulario.sApellido = "";
        //    $scope.formulario.CURP = "";
        //    $scope.formulario.fecNac = "";
        //    $scope.formulario.gender = "";
        //    $scope.formulario.emailContacto = "";
        //    $scope.formulario.Celular = null;
        //    $scope.formulario.CelularContacto = "";
        //    $scope.formulario.codPostDom = "";
        //    $scope.formulario.coloniaDom = "";
        //    $scope.formulario.domicilioCalle = "";
        //    $scope.formulario.entreCalleDom = "";
        //    $scope.formulario.yearResidencia = null;
        //}
        //else {
        $scope.CliForm = $scope.newCli;
        $scope.formulario.RFC = $scope.CliForm[0].rfc;
        $scope.formulario.pNombre = $scope.CliForm[0].primer_nombre;
        $scope.formulario.sNombre = $scope.CliForm[0].segundo_nombre;
        $scope.formulario.pApellido = $scope.CliForm[0].primer_apellido;
        $scope.formulario.sApellido = $scope.CliForm[0].segundo_apellido;
        $scope.formulario.CURP = $scope.CliForm[0].curp;
        $scope.formulario.fecNac = $scope.CliForm[0].fecha_nacimiento;
        //$scope.formulario.gender = $scope.CliForm[0].genero;
        $scope.formulario.emailContacto = $scope.CliForm[0].email;
        //$scope.formulario.Celular = ($scope.CliForm[0].celular);
        $scope.formulario.CelularContacto = ($scope.CliForm[0].celular)
        $scope.formulario.codPostDom = $scope.CliForm[0].codigo_postal;
        $scope.formulario.coloniaDom = $scope.CliForm[0].colonia_Domicilio;
        $scope.formulario.domicilioCalle = $scope.CliForm[0].domicilio_Calle;
        $scope.formulario.entreCalleDom = $scope.CliForm[0].entre_calles_domicilio;
        $scope.formulario.yearResidencia = Number($scope.CliForm[0].tiempo_residencia);
        $scope.formulario.nacionalidad = $scope.CliForm[0].nacionalidad
        if ($scope.formulario.codPostDom) {
            $scope.analizaCodigoPostal($scope.formulario.codPostDom);
        }
        $scope.analizaCurp($scope.formulario.CURP);
        $scope.selecCliente = true;
        $scope.formulario.cliente_siebel = 1;
        $scope.closeModal('modalRfcs');
        //}
    };
    $scope.rfc_ant = "";
    $scope.selecCliente = false;
    $scope.buscarCliente = function () {
        var valid = $scope.cadenaValida($scope.formulario.RFC, 13);
        if ($scope.formulario.RFC && valid && ($scope.rfc_ant != $scope.formulario.RFC)) {
            $scope.rfc_ant = $scope.formulario.RFC;
            $scope.openModal('loadCliente');
            BayportService.buscarCliente($scope.formulario.RFC).then(function (d) {
                if (d.data.estatus != "000") {
                    $scope.formulario.pNombre = "";
                    $scope.formulario.sNombre = "";
                    $scope.formulario.pApellido = "";
                    $scope.formulario.sApellido = "";
                    $scope.formulario.CURP = "";
                    $scope.formulario.fecNac = "";
                    $scope.formulario.gender = "";
                    $scope.formulario.emailContacto = "";
                    $scope.formulario.Celular = null;
                    $scope.formulario.CelularContacto = "";
                    $scope.formulario.codPostDom = "";
                    $scope.formulario.coloniaDom = "";
                    $scope.formulario.domicilioCalle = "";
                    $scope.formulario.entreCalleDom = "";
                    $scope.formulario.yearResidencia = null;
                    $scope.closeModal('loadCliente');
                    alert('Error al buscar el Cliente. ' + d.data.descripcionMovimiento);
                }
                else {
                    $scope.closeModal('loadCliente');
                    $scope.Clis = d.data;
                    $scope.openModal('modalRfcs');
                }
            }, function (err) {
                alert('Error al buscar el Cliente. ', err)
            });
        } else {
            if (!valid) {
                $('#valid-datos1').addClass('text-danger');
                $('#valid-datos1').text('Ha ingresado más de 13 carácteres. Por favor revise');
            }
        }
    }
    $scope.loadDocumentos = function () {
        if ($scope.formulario.Dependencia) {
            BayportService.getDocumentosOriginacion($scope.formulario.Dependencia, $scope.formulario.producto).then(function (d) {
                $scope.LisDocumentsCustomer = d.data;
                if (!$scope.compra) {
                    $scope.LisDocumentsCustomer.ListDocumentos = $scope.LisDocumentsCustomer.ListDocumentos.filter(d => d.compra != 1);
                }
                console.log($scope.LisDocumentsCustomer);
            });
        }
    }
    $scope.uploadedFileCustomer = function (element) {
        $scope.$apply(function ($scope) {
            $scope.filesCustomer = element.files;
            var boton = element.id;
            console.log(boton);
            var prueba = "#" + boton;
            var nombreArchivo = element.files["0"].name;
            var nombreLabel = "#data-" + boton;
            $(nombreLabel).text(nombreArchivo);
        });
    }
    $scope.getPaises = function () {
        BayportService.getPais().then(function (d) {
            $scope.PaisesList = d.data;
        });
    }
    $scope.saveFormulario = function (tab) {
        console.log(tab, $scope.formulario);
        BayportService.saveFormulario(tab, $scope.formulario).then(function (d) {
            if (d.data.msg.errorCode == '0') {
                $scope.formulario.folderNumber = ($scope.formulario.folderNumber) ? $scope.formulario.folderNumber : d.data.listSolicitudes[0].folderNumber;
                $scope.formulario.cartera = d.data.listSolicitudes[0].cartera;
            } else {
                alert('Puede que algunos Campos no se guarden');
            }
        });
    }
    $scope.getEntidadesFedN = function (pais) {
        BayportService.getEntidadPais(pais).then(function (d) {
            $scope.ListEntidades = d.data;
        });
    }
    $scope.getEntidadesFedR = function (pais) {
        BayportService.getEntidadPais(pais).then(function (d) {
            $scope.ListEntidadesR = d.data;
        });
    }
    $scope.municipioR = function (pais, entidad) {
        BayportService.getMunicipioEntidad(pais, entidad).then(function (d) {
            $scope.listMinucipios = d.data;
        })
    }
    $scope.municipioDom = function (pais, entidad) {
        BayportService.getMunicipioEntidad(pais, entidad).then(function (d) {
            $scope.listMinucipiosDom = d.data;
        })
    }
    $scope.EmpresasTelefonicas = function () {
        BayportService.getEmpresaTelefonica().then(function (d) {
            $scope.EmpresasTelefonica = d.data;
        });
    }
    $scope.DownloadDocFiles = function (url) {
        BayportService.DownloadDocFiles(url).then(function (d) {
            console.log('DownloadDocFiles ' + d.data.virtualpath);
            d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') :  d.data.virtualpath;

            $window.open(d.data.virtualpath, '_blank', '');
        }, function (error) {
            alert('Error GetDocumentsxAsesorxState!');
        });
    };
    $scope.openModal = function (modal, documento) {
        $scope.docLoad = documento;
        console.log($scope.docLoad);
        $('#' + modal).modal('show');
    }
    $scope.closeModal = function (modal) {
        $('#' + modal).modal('toggle');
    }
    $scope.uploadedFileOriginacion = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            var boton = element.id;
            var prueba = "#" + boton
            $scope.docLoad.nombre = boton;
            $scope.ArchivoOriginacion = element.files["0"].name;
            var nombreLabel = "#data-docOriginacion";
            $(nombreLabel).text($scope.ArchivoOriginacion);
                var reader = new FileReader();
                reader.onload = function (e) {
                    $scope.$apply(function () {
                        $scope.file64 = e.target.result.split("base64,")[1];
                    });
                }
                reader.readAsDataURL($scope.file);
                var value = element.files[0];
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$scope.file64 = e.target.result.split("base64,")[1];
                    $scope.originacionDoc.file = e.target.result.split("base64,")[1];
                    var fileCom = atob($scope.originacionDoc.file);
                    switch ($scope.file.type) {
                        case 'application/pdf':
                            if (fileCom.indexOf('%PDF-1.') === -1) {
                                alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                                $(nombreLabel).text('');
                                document.getElementById($scope.docLoad.nombre).value = '';
                                return;
                            }
                            break;
                        case 'image/png':
                            if (fileCom.indexOf('PNG') === -1) {
                                alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                                $(nombreLabel).text('');
                                document.getElementById($scope.docLoad.nombre).value = '';
                                return;
                            }
                            break;
                        case 'image/jpeg':
                            if (fileCom.indexOf('JFIF') > -1 && ($scope.file.name.indexOf('.jpg') === -1 && $scope.file.name.indexOf('.jpeg') === -1)) {
                                alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                                $(nombreLabel).text('');
                                document.getElementById($scope.docLoad.nombre).value = '';
                                return;
                            }
                            break;
                        default:
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById($scope.docLoad.nombre).value = '';
                            return;
                            break;
                    }
                    $scope.originacionDoc.file = null;
                    $scope.originacionDoc.nombreDoc = $scope.ArchivoOriginacion;
                    $scope.originacionDoc.codigo_doc = $scope.docLoad.cod_documento;
                    $scope.originacionDoc.firma = $scope.docLoad.firma;
                    $scope.originacionDoc.expedienteCompleto = 0;
                    $scope.subirDocOriginacion($scope.originacionDoc);
                }
                reader.readAsDataURL(value);
        });
    }
    $scope.uploadedFileOriginacionCompra = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            console.log($scope.file);
            var boton = element.id;
            $scope.ArchivoOriginacion = element.files["0"].name;
            var nombreLabel = "#data-docOriginacionCompra";
            $(nombreLabel).text($scope.ArchivoOriginacion);
                var reader = new FileReader();
                reader.onload = function (e) {
                    $scope.$apply(function () {
                        $scope.file64 = e.target.result.split("base64,")[1];
                    });
                }
                reader.readAsDataURL($scope.file);
                var value = element.files[0];
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$scope.file64 = e.target.result.split("base64,")[1];
                    $scope.originacionDoc.file = e.target.result.split("base64,")[1];
                    var fileCom = atob($scope.originacionDoc.file);
                    switch ($scope.file.type) {
                        case 'application/pdf':
                            console.log('hola')
                            if (fileCom.indexOf('%PDF-1.') === -1) {
                                alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                                $(nombreLabel).text('');
                                document.getElementById(boton).value = '';
                                return;
                            }
                            break;
                        case 'image/png':
                            if (fileCom.indexOf('PNG') === -1) {
                                alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                                $(nombreLabel).text('');
                                document.getElementById(boton).value = '';
                                return;
                            }
                            break;
                        case 'image/jpeg':
                            if (fileCom.indexOf('JFIF') > -1 && ($scope.file.name.indexOf('.jpg') === -1 && $scope.file.name.indexOf('.jpeg') === -1)) {
                                alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                                $(nombreLabel).text('');
                                document.getElementById(boton).value = '';
                                return;
                            }
                            break;
                        default:
                            alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf, .png o .jpg');
                            $(nombreLabel).text('');
                            document.getElementById(boton).value = '';
                            return;
                            break;
                    }
                    $scope.originacionDoc.file = null;
                    $scope.originacionDoc.nombreDoc = $scope.ArchivoOriginacion;
                    $scope.originacionDoc.nombre_cartera = $scope.docLoad.nombreDoc;
                    $scope.originacionDoc.codigo_doc = $scope.docLoad.codigo_doc;
                    $scope.originacionDoc.codigo = $scope.docLoad.codigo;
                    $scope.originacionDoc.firma = 1
                    $scope.originacionDoc.expedienteCompleto = 0;
                    $scope.originacionDoc.path = $scope.docLoad.path;
                    $scope.docLoad.nombre = boton;
                    $scope.subirDocOriginacionCompra($scope.originacionDoc, boton);
                }
                reader.readAsDataURL(value);
        });
    }
    $scope.uploadedFileOriginacionExpediente = function (element) {
        $scope.$apply(function ($scope) {
            $scope.file = element.files[0];
            var boton = element.id;
            var prueba = "#" + boton;
            $scope.ArchivoOriginacion = element.files["0"].name;
            var nombreLabel = "#data-expediente_Completo";
            $(nombreLabel).text($scope.ArchivoOriginacion);
            if ($scope.file.type == 'application/pdf') {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $scope.$apply(function () {
                        $scope.file64 = e.target.result.split("base64,")[1];
                    });
                }
                reader.readAsDataURL($scope.file);
                var value = element.files[0];
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$scope.file64 = e.target.result.split("base64,")[1];
                    var formData = new FormData();
                    $scope.originacionDoc.file = e.target.result.split("base64,")[1];
                    var fileCom = atob($scope.originacionDoc.file);
                    if (fileCom.indexOf('%PDF-1.') == -1) {
                        alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf');
                        $(nombreLabel).text('');
                        document.getElementById(boton).value = '';
                        return;
                    }
                    //$scope.originacionDoc.file = formData;
                    $scope.originacionDoc.file = null;
                    $scope.originacionDoc.nombreDoc = $scope.ArchivoOriginacion;
                    $scope.originacionDoc.codigo_doc = null;
                    $scope.originacionDoc.firma = 0;
                    $scope.originacionDoc.expedienteCompleto = 1;
                    
                }
                reader.readAsDataURL(value);
            } else {
                alert('Error El Archivo Cargado no es valido. Favor Cargue un archvio .pdf');
                $(nombreLabel).text('');
                document.getElementById(boton).value = '';
            }
        });
    }

    $scope.subirDocOriginacion = function (doc) {

        doc.folder = $scope.formulario.folderNumber;
        doc.producto = $scope.formulario.producto;
        doc.dependencia = $scope.formulario.Dependencia;
        var archivo = document.getElementById($scope.docLoad.nombre).files[0]
        var fdata = new FormData();
        fdata.append('file', archivo)
        fdata.append('firma', doc.firma);
        fdata.append('codigo_doc', doc.codigo_doc);
        fdata.append('nombreDoc', doc.nombreDoc);
        fdata.append('folder', doc.folder);
        fdata.append('dependencia', doc.dependencia);
        fdata.append('producto', doc.producto);
        BayportService.guardaDocumentoOriginacion(fdata).then(function (d) {
            if (d.data.msg.errorCode != '0') {
                alert(d.data.msg.errorMessage);
                var nombreLabel = "#data-docOriginacion";
                $(nombreLabel).text('');
                document.getElementById($scope.docLoad.nombre).value = '';
                $scope.originacionDoc = {
                    fila: "",
                    nombreDoc: "",
                    folder: null,
                    codigo_doc: null,
                    dependencia: null,
                    producto: null,
                    firma: null
                };
            } else {
                $scope.closeModal('modalDocumentos');
                $scope.saveFormulario('datos');
                alert('Documento cargado con exito.');
                $scope.originacionDoc = {
                    fila: "",
                    nombreDoc: "",
                    folder: null,
                    codigo_doc: null,
                    dependencia: null,
                    producto: null,
                    firma: null
                };
                var nombreLabel = "#data-docOriginacion";
                $(nombreLabel).text('');
                document.getElementById($scope.docLoad.nombre).value = '';
                $scope.enviarSolicitud = true;
            }
        }, function (err) {
            alert('No se púdo Cargar el Documento');
            document.getElementById($scope.docLoad.nombre).value = '';
        });
    }
    $scope.subirDocOriginacionCompra = function (doc, boton) {
        console.log(doc)
        doc.folder = $scope.formulario.folderNumber;
        doc.producto = $scope.formulario.producto;
        doc.dependencia = $scope.formulario.Dependencia;
        var archivo = document.getElementById(boton).files[0]
        var fdata = new FormData();
        fdata.append('file', archivo)
        fdata.append('firma', doc.firma);
        fdata.append('codigo_doc', doc.codigo_doc);
        fdata.append('nombreDoc', doc.nombreDoc);
        fdata.append('folder', doc.folder);
        fdata.append('dependencia', doc.dependencia);
        fdata.append('producto', doc.producto);
        fdata.append('path', doc.path);
        fdata.append('codigo', doc.codigo);
        fdata.append('nombre_cartera', doc.nombre_cartera);

        BayportService.guardaDocumentoOriginacionCompra(fdata).then(function (d) {
            if (d.data.msg.errorCode != '0') {
                alert(d.data.msg.errorMessage);
                var nombreLabel = "#data-docOriginacionCompra";
                $(nombreLabel).text('');
                document.getElementById($scope.docLoad.nombre).value = '';
                $scope.originacionDoc = {
                    fila: "",
                    nombreDoc: "",
                    folder: null,
                    codigo_doc: null,
                    dependencia: null,
                    producto: null,
                    firma: null
                };
            } else {
                $scope.closeModal('modalDocumentosCompra');
                $scope.saveFormulario('datos');
                alert('Documento cargado con exito.');
                $scope.originacionDoc = {
                    fila: "",
                    nombreDoc: "",
                    folder: null,
                    codigo_doc: null,
                    dependencia: null,
                    producto: null,
                    firma: null
                };
                var nombreLabel = "#data-docOriginacionCompra";
                $(nombreLabel).text('');
                document.getElementById(boton).value = '';
                $scope.enviarSolicitud = true;
            }
        }, function (err) {
            alert('No se pudo Cargar el Documento');
        });
    }
    $scope.subirDocOriginacionExpediente = function () {
        $scope.originacionDoc.folder = $scope.formulario.folderNumber;
        $scope.originacionDoc.producto = $scope.formulario.producto;
        $scope.originacionDoc.dependencia = $scope.formulario.Dependencia;
        var fdata = new FormData();
        var archivo;
        if (document.getElementById('expediente_Completo').files[0] != undefined) {
            var archivo = document.getElementById('expediente_Completo').files[0];
            fdata.append("expediente", archivo);
            fdata.append('folder', $scope.formulario.folderNumber);
            fdata.append('producto', $scope.formulario.producto);
            fdata.append('dependencia', $scope.formulario.Dependencia);
            BayportService.guardaDocumentoOriginacion1(fdata).then(function (d) {
                if (d.data.msg.errorCode != '0') {
                    alert(d.data.msg.errorMessage);
                    document.getElementById('expediente_Completo').value = '';
                    $('#data-expediente_Completo').text('');
                } else {
                    alert('Documento cargado con exito.');
                    $scope.originacionDoc = {
                        fila: "",
                        nombreDoc: "",
                        folder: null,
                        codigo_doc: null,
                        dependencia: null,
                        producto: null,
                        firma: null
                    };
                    $('#data-expediente_Completo').text('');
                    $scope.enviarSolicitud = true;
                    $scope.findExpCompleto($scope.formulario.folderNumber);
                    document.getElementById('expediente_Completo').value = '';
                }
            }, function (err) {
                alert('No se púdo Cargar el Documento');
            });
        } else {
            alert('Debe Cargar un Documento');
        }
    }
    $scope.buscarDocumentos = function (item, folder) {
        BayportService.buscarDocumentos(item.cod_documento, folder).then(function (d) {
            if (d.data.msg.errorCode == '0') {
                $scope.findDocumentos = item;
                console.log($scope.formulario.estado);
                console.log($scope.findDocumentos)
                
                $scope.DocumentosCargados = d.data.ListDocumentos;
            } else {
                alert('Error al consultar los Documentos. ', d.data.msg.errorMessage);
            }
        });
    }
    $scope.eliminarDoc = function (documento) {
        BayportService.eliminaDocumentosOriginacion(documento.codigo).then(function (d) {
            if (d.data.errorCode != '0') {
                alert(d.data.errorMessage);
            } else {
                var ind = $scope.DocumentosCargados.indexOf(documento);
                $scope.DocumentosCargados.splice(ind);
                $scope.docEliminar = {};
                $scope.closeModal('eliminarDocumento');
            }
        });
    }
    $scope.asignarDoc = function (item) {
        $scope.docEliminar = item;
    }
    $scope.crearDoc = function (doc, folder, dependencia, producto, compra) {
        var cambio = {};
        if ($scope.formulario.estado != '') {
            cambio = $scope.cambioFormulario($scope.formulario, $scope.formularioAlterno);
        }
        if (compra === 0) {
            BayportService.documentoOriginacion(dependencia, producto, folder, doc).then(function (d) {
                if (d.data != "") {
                    d.data = (d.data.indexOf('http://originacionbayport') > -1) ? d.data.replace('http', 'https') : d.data;
                    $window.open(d.data, '_blank', '');
                } else {
                    alert("Error al General el Documento")
                }
            });
        } else {
            BayportService.documentoCartera(dependencia, producto, folder, doc).then(function (d) {
                if (d.data.length > 0) {
                    for (let item of d.data) {
                        console.log(item)
                        item = (item.indexOf('http://originacionbayport') > -1) ? item.replace('http', 'https') : item;
                        $window.open(item, '_blank', '');
                    }
                } else {
                    alert("Error al General el Documento")
                }
            });
        }
        if (Object.keys(cambio).length > 0) {
            for (var key of Object.keys(cambio)) {
                $scope.formularioAlterno[key] = cambio[key];
            }
        }

    }
    $scope.getListadoSolicitudes = function () {
        $window.localStorage["formulario"] = "";
        BayportService.listSolicitudes().then(function (d) {
            $scope.ListSolicitudes = d.data;
            for (let item of $scope.ListSolicitudes.listSolicitudes) {
                item.estado = (item.estado == "") ? 'NUEVO' : item.estado;
            }
        });
        $scope.searchText = $window.localStorage["estado"];

    }
    $scope.buscaBanco = function () {
        if ($scope.formulario.ClabeDisp.length == $scope.tamMedio) {
            var cod = $scope.formulario.ClabeDisp.substring(0, 3);
            if ($scope.tamMedio == 18) {
                BayportService.getIdBanco(cod).then(function (d) {
                    if (d.data.ListBancos.length > 0) {
                        $scope.formulario.NombreBanco = cod;

                        $scope.formulario.NumCuentaBanc = $scope.formulario.ClabeDisp.substring(6, 17);

                    } else {
                        alert('Número de cuenta equivocado');
                        $scope.formulario.ClabeDisp = "";
                        $scope.formulario.NombreBanco = "";
                    }
                });
            } else {
                $scope.formulario.ClabeDisp = "";
                $scope.formulario.NombreBanco = "";
            }
        } else {
            $scope.formulario.ClabeDisp = "";
            $scope.formulario.NombreBanco = "";
            $scope.formulario.NumCuentaBanc = "";
        }
    }
    $scope.buscaBancoAlt = function () {
        if ($scope.formulario.ClabeDispAlt.length == $scope.tamMedioAlt) {
            var cod = $scope.formulario.ClabeDispAlt.substring(0, 3);
            if ($scope.tamMedioAlt == 18) {
                BayportService.getIdBanco(cod).then(function (d) {
                    if (d.data.ListBancos.length > 0) {
                        $scope.formulario.NombreBancoAlt = cod;

                        $scope.formulario.NumCuentaBancAlt = $scope.formulario.ClabeDispAlt.substring(6, 17);

                    } else {
                        alert('Número de cuenta equivocado');
                        $scope.formulario.ClabeDispAlt = "";
                        $scope.formulario.NombreBancoAlt = "";
                        $scope.formulario.NumCuentaBancAlt = "";
                    }
                });
            } else {
                $scope.formulario.NombreBancoAlt = "";
                $scope.formulario.NumCuentaBancAlt = "";
            }
        } else {
            $scope.formulario.ClabeDispAlt = "";
            $scope.formulario.NombreBancoAlt = "";
            $scope.formulario.NumCuentaBancAlt = "";
        }
    }
    $scope.buscaBancoAlt2 = function () {
        if ($scope.formulario.ClabeDispAlt2.length == $scope.tamMedioAlt2) {
            var cod = $scope.formulario.ClabeDispAlt2.substring(0, 3);
            if ($scope.tamMedioAlt2 == 18) {
                BayportService.getIdBanco(cod).then(function (d) {
                    if (d.data.ListBancos.length > 0) {
                        $scope.formulario.NombreBancoAlt2 = cod;

                        $scope.formulario.NumCuentaBancAlt2 = $scope.formulario.ClabeDispAlt2.substring(6, 17);

                    } else {
                        alert('Número de cuenta equivocado');
                        $scope.formulario.ClabeDispAlt2 = "";
                        $scope.formulario.NombreBancoAlt2 = "";
                        $scope.formulario.NumCuentaBancAlt2 = "";
                    }
                });
            } else {
                $scope.formulario.NombreBancoAlt2 = "";
                $scope.formulario.NumCuentaBancAlt2 = "";
            }
        } else {
            $scope.formulario.ClabeDispAlt2 = "";
            $scope.formulario.NombreBancoAlt2 = "";
            $scope.formulario.NumCuentaBancAlt2 = "";
        }
    }
    $scope.soloFirmas = function () {
        BayportService.DocSoloFirmas($scope.formulario.Dependencia, $scope.formulario.producto, $scope.formulario.folderNumber).then(
            function (d) {
                if (d.data.virtualpath != "" && d.data.virtualpath != null) {
                    console.log('soloFirmas ' + d.data.virtualpath);
                    d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') : d.data.virtualpath;
                    console.log()
                    $window.open(d.data.virtualpath, '_blank', '');
                }
            }, function (err) {
                alert(err);
                console.log('prueba',err)
            });
        
    }
    $scope.expedientillo = function () {
        var cambioDoc = 0;
        var cambio = {};
        if ($scope.formulario.estado != '') {
            cambio = $scope.cambioFormulario($scope.formulario, $scope.formularioAlterno);
            cambioDoc = (Object.keys(cambio).length == 0) ? 0 : 1;
        }
        BayportService.Expedientillo($scope.formulario.Dependencia, $scope.formulario.producto, $scope.formulario.folderNumber, cambioDoc).then(function (d) {
            if (d.data.virtualpath != "" && d.data.virtualpath != null) {
                if (Object.keys(cambio).length > 0) {
                    for (var key of Object.keys(cambio)) {
                        $scope.formularioAlterno[key] = cambio[key];
                    }
                }
                
                d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') :  d.data.virtualpath;

                $window.open(d.data.virtualpath, '_blank', '');
            }
        });     
    }
    $scope.allDocs = function () {
        var cambioDoc = 0;
        var cambio = {};
        if ($scope.formulario.estado != '') {
            cambio = $scope.cambioFormulario($scope.formulario, $scope.formularioAlterno);
            cambioDoc = (Object.keys(cambio).length == 0) ? 0 : 1;
        }
        BayportService.allDocs($scope.formulario.Dependencia, $scope.formulario.producto, $scope.formulario.folderNumber, cambioDoc).then(function (d) {
            if (d.data.virtualpath != "" && d.data.virtualpath != null) {
                if (Object.keys(cambio).length > 0) {
                    for (var key of Object.keys(cambio)) {
                        $scope.formularioAlterno[key] = cambio[key];
                    }
                }
                d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') :  d.data.virtualpath;
                $window.open(d.data.virtualpath, '_blank', '');
            }
        });
    }
    $scope.impresionAll = function () {
        BayportService.impresion($scope.formulario.Dependencia, $scope.formulario.producto, $scope.formulario.folderNumber).then(function (d) {
            if (d.data.virtualpath != "" && d.data.virtualpath != null) {
                console.log('impresionAll ' + d.data.virtualpath);
                d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') :  d.data.virtualpath;
                $window.open(d.data.virtualpath, '_blank', '');
            }
        });
    }
    $scope.cambiaEstado = function () {
        $scope.formulario.expediente_completo = Number($scope.expedienteCompletoLoad);
        $scope.saveFormulario('datos');
        if ($scope.formulario.expediente_completo == 1) {
            $scope.findExpCompleto($scope.formulario.folderNumber);
        } else {
            $scope.DocumentosCargados = null;
        }
    }
    $scope.getIdSolicitud = function (folder) {
        BayportService.getIdSolicitud(folder).then(function (d) {
            if (d.data.msg.errorCode == "0") {
                //$scope.formulario = d.data
                $window.localStorage["formulario"] = JSON.stringify(d.data);
                $window.location.href = '/Profile/index';
            }
            else {
                alert(d.data.errorMessage);
            }
        })
    }
    $scope.rechazaroferta = function () {
        $window.localStorage["formulario"] = "";
        $scope.formulario = {};
        $('#tabs a[href="#solicitud"]').tab('show');
        $('#solicitud li.disabled a').removeAttr('data-toggle');
        $('#ocupacion li.disabled a').removeAttr('data-toggle');
        $('#domicilio li.disabled a').removeAttr('data-toggle');
        $('#referencias li.disabled a').removeAttr('data-toggle');
        $('#medios li.disabled a').removeAttr('data-toggle');
        $('#cartera li.disabled a').removeAttr('data-toggle');
        $('#documentos li.disabled a').removeAttr('data-toggle');
        $scope.closeModal('myModalOfert');
    }

    $scope.seleccionarSeguro = function (seguro) {
        console.log($scope.formulario.monto)
        if ($scope.ofertaaceptada.PolicyValue) {
            $scope.formulario.monto += $scope.ofertaaceptada.PolicyValue;
        }
        $scope.ofertaaceptada = seguro;
        $scope.formulario.monto -= $scope.ofertaaceptada.PolicyValue;
        $scope.formulario.codePlan = $scope.ofertaaceptada.PlanCode;
        $scope.formulario.planValue = $scope.ofertaaceptada.PolicyValue;
    }
    $scope.setNoAplicaSeguro = function () {
        if ($scope.ofertaaceptada.PolicyValue) {
            $scope.formulario.monto += $scope.ofertaaceptada.PolicyValue;
        }
        $scope.ofertaaceptada = {};
        $scope.formulario.codePlan = "";
        $scope.formulario.planValue = "";
    };
    $scope.RecalcularOferta = function () {
        $scope.closeModal('myModalOfert');
        $scope.formulario.codePlan = null;
        $scope.formulario.planValue = null;
        $scope.formulario.tiene_seguro = null;
    }
    $scope.docs = function () {
        $('#tabs a[href="#documentos1"]').tab('show');
        if ($scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == 'Devolucion') {
            $scope.enviarSolicitud = true;
        }
        $scoep.procesarPerfil = "Enviar a Auxiliar Administrativo";
    }
    $scope.permiteBeneficiarios = [];
    $scope.AceptarOferta = function (acepta) {
        if (acepta) {
            $scope.formulario.tiene_seguro = ($scope.checked) ? 0 : 1;
            $scope.permiteBeneficiarios = [];
            BayportService.saveFormulario('solicitud', $scope.formulario).then(function (d) { });
            BayportService.aceptaOferta($scope.formulario).then(function (d) {
                if (d.data.errorCode == '0') {
                    if ($scope.formulario.tiene_seguro == 1) {
                        BayportService.beneficiariosPoliza($scope.formulario.folderNumber).then(function (d) {
                            $scope.closeModal('myModalOfert');
                            $scope.permiteBeneficiarios = d.data.split('|');
                            $scope.formulario.id_poliza = $scope.permiteBeneficiarios[2];
                            if ($scope.permiteBeneficiarios[0] == '1' || $scope.permiteBeneficiarios[1] == '1') {
                                $scope.openModal('myModalBeneficiarios');
                            }
                            else {
                                $('#tabs a[href="#documentos"]').tab('show');
                                if ($scope.formulario.estado == '' || $scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == 'Devolucion') {
                                    $scope.enviarSolicitud = true;
                                }
                                $scope.procesarPerfil = "Enviar a Auxiliar Administrativo";
                            }
                        });
                    } else {
                        $scope.closeModal('myModalOfert');
                        $('#tabs a[href="#documentos"]').tab('show');
                        if ($scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == '' || $scope.formulario.estado == 'Devolucion') {
                            $scope.enviarSolicitud = true;
                        }
                        $scope.procesarPerfil = "Enviar a Auxiliar Administrativo";
                    }
                } else {
                    alert(d.data.errorMessage);
                    $scope.closeModal('myModalOfert');
                    $('#tabs a[href="#documentos"]').tab('show');
                    if ($scope.formulario.estado == 'NUEVA' || $scope.formulario.estado == '' || $scope.formulario.estado == 'Devolucion') {
                        $scope.enviarSolicitud = true;
                    }
                    $scope.procesarPerfil = "Enviar a Auxiliar Administrativo";
                }
            });
        }
    }
    $scope.calcularOferta = function () {
        setTimeout(function () {
            var valid = true;
            if ($scope.formulario.Dependencia && $scope.formulario.monto && $scope.formulario.periodo
                && $scope.formulario.plazo && $scope.formulario.LBase) {
                if ($scope.ProductosList.ListProductosDependencia.length > 0) {
                    if (!$scope.formulario.producto) {
                        valid = false;
                    }
                }
                if (valid) {
                    BayportService.calcularOferta($scope.formulario).then(function (d) {
                        if (d.data.estatus == '0') {
                            $scope.formulario.dscto = Number(Number(d.data.descuento).toFixed(2));
                            $scope.formulario.tAnual = Number(Number(d.data.tasa_anual).toFixed(2));
                            $scope.formulario.cat = Number(Number(d.data.cat).toFixed(2));
                            $scope.formulario.quincenaDscto = d.data.qna_descuento;
                            $scope.formulario.fchPrPago = d.data.fecha_1pago;
                            $scope.formulario.fchUltPago = (d.data.fecha_upago);
                            $scope.formulario.cPago = Number(Number(d.data.capacidad_pago).toFixed(2));
                            $scope.formulario.mMaxPlaz = Number(Number(d.data.monto_maxp).toFixed(2));
                        } else {
                            $scope.TituloModal = "Error al Calcular Valores";
                            $scope.msgCabecera = "Error Consultando Valores de Oferta";
                            $scope.msgDetalle = d.data.estatus + " " + d.data.descripcionMovimiento;
                            $scope.openModal('myModalMensage');
                        }
                    });
                }
            }
        }, 500)

    }
    $scope.calculaEdad = function (ind) {
        anio = new Date().getFullYear();
        anio2 = '';
        if (ind == 2) {
            anio2 = $scope.beneficiarioPoliza.fechaNacimiento.substring(6, 10);
            $scope.beneficiarioPoliza.edad = Number(anio) - Number(anio2);
        }
        if (ind == 1) {
            anio2 = $scope.beneficiarioPolizaFamiiliar.fechaNacimiento.substring(6, 10);
            $scope.beneficiarioPolizaFamiiliar.edad = Number(anio) - Number(anio2);
        }
    }
    $scope.listParentesco = function () {
        BayportService.Listparentesco().then(function (d) {
            $scope.Parentescos = d.data;
        });
    }

    $scope.valorAseguradosPoliza = 0;
    $scope.validaValorAsegurados = function (valor, ind) {
        if ($scope.valorAseguradosPoliza == 0) {
            if (valor <= $scope.formulario.planValue) {
                $('#valid-familiarPoliza').removeClass('text-danger');
                $('#valid-BenefiarioPoliza').removeClass('text-danger');
                $('#valid-familiarPoliza').text('');
                $('#valid-BenefiarioPoliza').text('');
            } else {
                if (ind == 1) {
                    $('#valid-familiarPoliza').addClass('text-danger');
                    $('#valid-familiarPoliza').text('El valor debe ser menor o igual al valor de la Poliza');
                    console.log($scope.formulario);
                } else {
                    $('#valid-BenefiarioPoliza').addClass('text-danger');
                    $('#valid-BenefiarioPoliza').text('El valor debe ser menor o igual al valor de la Poliza');
                }
            }
        } else if ($scope.valorAseguradosPoliza + valor > $scope.formulario.planValue) {
            if (ind == 1) {
                $('#valid-familiarPoliza').addClass('text-danger');
                $('#valid-familiarPoliza').text('El valor debe ser menor o igual al valor de la Poliza');
            } else {
                $('#valid-BenefiarioPoliza').addClass('text-danger');
                $('#valid-BenefiarioPoliza').text('El valor debe ser menor o igual al valor de la Poliza');
            }
        } else {
            $('#valid-familiarPoliza').removeClass('text-danger');
            $('#valid-BenefiarioPoliza').removeClass('text-danger');
            $('#valid-familiarPoliza').text('');
            $('#valid-BenefiarioPoliza').text('');
        }
    }
    $scope.beneficiarioPoliza = {};
    $scope.beneficiarioPoliza.tipo = 2;
    $scope.registrarBeneficiario = function () {
        var mes = Number($scope.beneficiarioPoliza.fechaNacimiento.substring(3, 5))
        if (mes > 0 && mes < 13) {
            if ($scope.beneficiarioPoliza.tipoDoc && $scope.beneficiarioPoliza.rfc && $scope.beneficiarioPoliza.pNombre
                && $scope.beneficiarioPoliza.pApellido && $scope.beneficiarioPoliza.entidadF && $scope.beneficiarioPoliza.municipioF
                && $scope.beneficiarioPoliza.tefFijoBeneficiario && $scope.beneficiarioPoliza.celularBeneficiario && $scope.beneficiarioPoliza.vlrAseguradoBeneficiario
                && $scope.beneficiarioPoliza.fechaNacimiento && $scope.beneficiarioPoliza.gender && $scope.beneficiarioPoliza.parentezco) {
                if (($scope.valorAseguradosPoliza + $scope.beneficiarioPoliza.vlrAseguradoBeneficiario) <= $scope.formulario.planValue) {
                    BayportService.guardarBeneficiario($scope.beneficiarioPoliza, $scope.formulario.id_poliza).then(function (d) {
                        if (d.data.errorCode == '0') {
                            $scope.openModal('agregarBeneficiario');
                            $scope.valorAseguradosPoliza += $scope.beneficiarioPoliza.vlrAseguradoBeneficiario;
                        } else {
                            alert(d.data.errorMessage);
                        }
                    });
                } else {
                    alert('No se puede exceder el valor de la Poliza.');
                }
            }
            else {
                alert('Todos los campos son requeridos');
            }
        }
        else {
            alert('El mes no puede ser mayor a 12 o menor a 1');
        }
    }
    $scope.beneficiarioPolizaFamiiliar = {};
    $scope.beneficiarioPolizaFamiiliar.tipo = 1;
    $scope.registrarFamiliar = function () {
        var mes = Number($scope.beneficiarioPolizaFamiiliar.fechaNacimiento.substring(3, 5))
        if (mes > 0 && mes < 13) {
            if ($scope.beneficiarioPolizaFamiiliar.tipoDoc && $scope.beneficiarioPolizaFamiiliar.rfc && $scope.beneficiarioPolizaFamiiliar.pNombre
                && $scope.beneficiarioPolizaFamiiliar.pApellido && $scope.beneficiarioPolizaFamiiliar.entidadF && $scope.beneficiarioPolizaFamiiliar.municipioF
                && $scope.beneficiarioPolizaFamiiliar.tefFijoBeneficiario && $scope.beneficiarioPolizaFamiiliar.celularBeneficiario && $scope.beneficiarioPolizaFamiiliar.vlrAseguradoBeneficiario
                && $scope.beneficiarioPolizaFamiiliar.fechaNacimiento && $scope.beneficiarioPolizaFamiiliar.gender && $scope.beneficiarioPolizaFamiiliar.parentezco) {
                if (($scope.valorAseguradosPoliza + $scope.beneficiarioPolizaFamiiliar.vlrAseguradoBeneficiario) <= $scope.formulario.planValue) {
                    BayportService.guardarBeneficiario($scope.beneficiarioPolizaFamiiliar, $scope.formulario.id_poliza).then(function (d) {
                        if (d.data.errorCode == '0') {
                            $scope.valorAseguradosPoliza += $scope.beneficiarioPolizaFamiiliar.vlrAseguradoBeneficiario;
                            $scope.openModal('agregarBeneficiario');
                        } else {
                            alert(d.data.errorMessage);
                        }
                    });
                } else {
                    alert('No se puede exceder el valor de la Poliza.');
                }
            }
            else {
                alert('Todos los campos son requeridos');
            }
        }
        else {
            alert('El mes no puede ser mayor a 12 o menor a 1');
        }
    }
    $scope.addBeneficiaro = function (valid) {
        if (valid) {
            $scope.beneficiarioPolizaFamiiliar = {};
            $scope.beneficiarioPolizaFamiiliar.tipo = 1;
            $scope.beneficiarioPoliza = {};
            $scope.beneficiarioPoliza.tipo = 2;
            $scope.closeModal('agregarBeneficiario');
        } else {
            $scope.closeModal('agregarBeneficiario');
            $scope.closeModal('myModalBeneficiarios');
            $('#tabs a[href="#documentos"]').tab('show');
            $scope.enviarSolicitud = true;
            $scope.procesarPerfil = "Enviar a Auxiliar Administrativo";
        }
    }
    $scope.validaRFCBeneficiario = function (rfc, ind) {
        //if (rfc) {
            var d = rfc.substring(8, 10);
            var m = rfc.substring(6, 8);
            var y = rfc.substring(4, 6);
            var y1 = new Date().getFullYear();
            var y2 = String(y1).substring(2, 4);
            if (ind == 1) {
                $scope.beneficiarioPolizaFamiiliar.fechaNacimiento = d + '/' + m + '/';
                $scope.beneficiarioPolizaFamiiliar.fechaNacimiento += ((Number(y) > Number(y2)) ? '19' : '20') + y;
                $scope.calculaEdad(1)
            }
            if (ind == 2) {
                $scope.beneficiarioPoliza.fechaNacimiento = d + '/' + m + '/';
                $scope.beneficiarioPoliza.fechaNacimiento += ((Number(y) > Number(y2)) ? '19' : '20') + y;
                $scope.calculaEdad(2)
            }
        //}
    }
    $scope.validaValor = function (ind) {
        console.log('prueba', ind)
        switch (ind) {
            case 1:
                if ($scope.formulario.LBase) {
                    if ($scope.formulario.LBase < 0) {
                        $('#valid-solicitud6').addClass('text-danger');
                        $('#valid-solicitud6').text('El valor debe ser mayor a 0');
                    } else {
                        $('#valid-solicitud6').removeClass('text-danger');
                        $('#valid-solicitud6').text('');
                    }
                } else {
                    $('#valid-solicitud6').removeClass('text-danger');
                    $('#valid-solicitud6').text('');
                }
                break;
            case 2:
                if ($scope.formulario.nPlazas) {
                    if ($scope.formulario.nPlazas < 0) {
                        $('#valid-solicitudPlaza').addClass('text-danger');
                        $('#valid-solicitudPlaza').text('El valor debe ser mayor a 0');
                    } else {
                        $('#valid-solicitudPlaza').removeClass('text-danger');
                        $('#valid-solicitudPlaza').text('');
                    }
                } else {
                    $('#valid-solicitudPlaza').removeClass('text-danger');
                    $('#valid-solicitudPlaza').text('');
                }
                break;
            case 3:
                if ($scope.formulario.cPago) {
                    if ($scope.formulario.cPago < 0) {
                        $('#valid-solicitud16').addClass('text-danger');
                        $('#valid-solicitud16').text('El valor debe ser mayor a 0');
                    } else {
                        $('#valid-solicitud16').removeClass('text-danger');
                        $('#valid-solicitud16').text('');
                    }
                } else {
                    $('#valid-solicitud16').removeClass('text-danger');
                    $('#valid-solicitud16').text('');
                }
                break;
            case 4:
                if ($scope.formulario.mMaxPlaz) {
                    if ($scope.formulario.mMaxPlaz < 0) {
                        $('#valid-solicitud17').addClass('text-danger');
                        $('#valid-solicitud17').text('El valor debe ser mayor a 0');
                    } else {
                        $('#valid-solicitud17').removeClass('text-danger');
                        $('#valid-solicitud17').text('');
                    }
                } else {
                    $('#valid-solicitud17').removeClass('text-danger');
                    $('#valid-solicitud17').text('');
                }
                break;
            case 5:
                if ($scope.formulario.yearResidencia) {
                    if ($scope.formulario.yearResidencia < 0) {
                        $('#valid-domicilio2').addClass('text-danger');
                        $('#valid-domicilio2').text('El valor debe ser mayor a 0');
                    } else {
                        $('#valid-domicilio2').removeClass('text-danger');
                        $('#valid-domicilio2').text('');
                    }
                } else {
                    $('#valid-domicilio2').removeClass('text-danger');
                    $('#valid-domicilio2').text('');
                }
                break;
            case 6:
                if ($scope.formulario.antiguedad) {
                    if ($scope.formulario.antiguedad < 0) {
                        $('#valid-ocupacion3').addClass('text-danger');
                        $('#valid-ocupacion3').text('El valor debe ser mayor a 0');
                    } else {
                        $('#valid-ocupacion3').removeClass('text-danger');
                        $('#valid-ocupacion3').text('');
                    }
                } else {
                    $('#valid-ocupacion3').removeClass('text-danger');
                    $('#valid-ocupacion3').text('');
                }
                break;
        }
    }
    $scope.getComentarios = function (folder) {
        BayportService.getComentarios(folder).then(function (d) {
            $scope.comentariosList = d.data;
        });
    }

    $scope.getDashBoard = function () {
        BayportService.getDashBoard().then(function (d) {
            $scope.dashBoardList = d.data;
        });
    }

    $scope.verListado = function (estado) {
        $window.localStorage["estado"] = estado;
        $window.location.href = '/Profile/Listado';
    }

    $scope.expedienteCompleto = function (item) {
        BayportService.allDocs(item.dependenciaCodigo, item.productoCodigo, item.folderNumber).then(function (d) {
            if (d.data.virtualpath != "" && d.data.virtualpath != null) {
                console.log('expedienteCompleto ' + d.data.virtualpath);
                d.data.virtualpath = (d.data.virtualpath.indexOf('http://originacionbayport') > -1) ? d.data.virtualpath.replace('http', 'https') :  d.data.virtualpath;

                $window.open(d.data.virtualpath, '_blank', '');
            }
        })
    }
    $scope.compra = false;
    $scope.ChangeSolicitud = function (codigo) {
        setTimeout(function () {
            $scope.tipoSolicitud = $scope.tipoSolicitudList.ListTipoSolicitud.filter(d => d.secuencia === codigo);
            if ($scope.tipoSolicitud.length > 0) {
                $scope.compra = ($scope.tipoSolicitud[0].tipoSolicitud.toUpperCase().indexOf('COMPRA') > -1) ? true : false;
            }
            if ($scope.LisDocumentsCustomer && $scope.LisDocumentsCustomer.ListDocumentos.length > 0) {
                $scope.loadDocumentos();
            }
        }, 1000);       
    };
    $scope.validaLongitud = function (valor, longitud, ind) {
        var text1 = 'Ha ingresado menos de ' + longitud + '. Por favor revise.';
        var text2 = 'Ha ingresado más de ' + longitud + '. Por favor revise.';
        switch (ind) {
            case 1:
                if (String(valor).length < longitud) {
                    $('#valid-domicilio11').addClass('text-danger');
                    $('#valid-domicilio11').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-domicilio11').addClass('text-danger');
                    $('#valid-domicilio11').text(text2);
                } else {
                    $('#valid-domicilio11').removeClass('text-danger');
                    $('#valid-domicilio11').text('');
                }
                break;
            case 2:
                if (String(valor).length < longitud) {
                    $('#valid-referencias4').addClass('text-danger');
                    $('#valid-referencias4').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-referencias4').addClass('text-danger');
                    $('#valid-referencias4').text(text2);
                } else {
                    $('#valid-referencias4').removeClass('text-danger');
                    $('#valid-referencias4').text('');
                }
                break;
            case 3:
                if (String(valor).length < longitud) {
                    $('#valid-referencias5').addClass('text-danger');
                    $('#valid-referencias5').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-referencias5').addClass('text-danger');
                    $('#valid-referencias5').text(text2);
                } else {
                    $('#valid-referencias5').removeClass('text-danger');
                    $('#valid-referencias5').text('');
                }
                break;
            case 4:
                if (String(valor).length < longitud) {
                    $('#valid-referencias11').addClass('text-danger');
                    $('#valid-referencias11').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-referencias11').addClass('text-danger');
                    $('#valid-referencias11').text(text2);
                } else {
                    $('#valid-referencias11').removeClass('text-danger');
                    $('#valid-referencias11').text('');
                }
                break;
            case 5:
                if (String(valor).length < longitud) {
                    $('#valid-referencias12').addClass('text-danger');
                    $('#valid-referencias12').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-referencias12').addClass('text-danger');
                    $('#valid-referencias12').text(text2);
                } else {
                    $('#valid-referencias12').removeClass('text-danger');
                    $('#valid-referencias12').text('');
                }
                break;
            case 6:
                if (String(valor).length < longitud) {
                    $('#valid-ocupacion13').addClass('text-danger');
                    $('#valid-ocupacion13').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-ocupacion13').addClass('text-danger');
                    $('#valid-ocupacion13').text(text2);
                } else {
                    $('#valid-ocupacion13').removeClass('text-danger');
                    $('#valid-ocupacion13').text('');
                }
                break;
            case 7:
                if (String(valor).length < longitud) {
                    $('#valid-solicitud21').addClass('text-danger');
                    $('#valid-solicitud21').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-solicitud21').addClass('text-danger');
                    $('#valid-solicitud21').text(text2);
                } else {
                    $('#valid-solicitud21').removeClass('text-danger');
                    $('#valid-solicitud21').text('');
                }
                break;
            case 8:
                if (String(valor).length < longitud) {
                    $('#valid-datos1').addClass('text-danger');
                    $('#valid-datos1').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-datos1').addClass('text-danger');
                    $('#valid-datos1').text(text2);
                } else {
                    $('#valid-datos1').removeClass('text-danger');
                    $('#valid-datos1').text('');
                }
                break;
            case 9:
                if (String(valor).length < longitud) {
                    $('#valid-datos6').addClass('text-danger');
                    $('#valid-datos6').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-datos6').addClass('text-danger');
                    $('#valid-datos6').text(text2);
                } else {
                    $('#valid-datos6').removeClass('text-danger');
                    $('#valid-datos6').text('');
                }
                break;
            case 10:
                if (String(valor).length < longitud) {
                    $('#valid-domicilio9').addClass('text-danger');
                    $('#valid-domicilio9').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-domicilio9').addClass('text-danger');
                    $('#valid-domicilio9').text(text2);
                } else {
                    $('#valid-domicilio9').removeClass('text-danger');
                    $('#valid-domicilio9').text('');
                }
                break;
            case 12:
                if (String(valor).length < longitud) {
                    $('#valid-medios4').addClass('text-danger');
                    $('#valid-medios4').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-medios4').addClass('text-danger');
                    $('#valid-medios4').text(text2);
                } else {
                    $('#valid-medios4').removeClass('text-danger');
                    $('#valid-medios4').text('');
                }
                break;
            case 13:
                if (String(valor).length < longitud) {
                    $('#valid-medios6').addClass('text-danger');
                    $('#valid-medios6').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-medios6').addClass('text-danger');
                    $('#valid-medios6').text(text2);
                } else {
                    $('#valid-medios6').removeClass('text-danger');
                    $('#valid-medios6').text('');
                }
                break;
            case 14:
                /*if (String(valor).length < longitud) {
                    $('#valid-ocupacion6').addClass('text-danger');
                    $('#valid-ocupacion6').text(text1);
                }*/
                if (String(valor).length > longitud) {
                    $('#valid-ocupacion6').addClass('text-danger');
                    $('#valid-ocupacion6').text(text2);
                } else {
                    $('#valid-ocupacion6').removeClass('text-danger');
                    $('#valid-ocupacion6').text('');
                }
                break;
            case 15:
                if (String(valor).length < longitud) {
                    $('#valid-medios2').addClass('text-danger');
                    $('#valid-medios2').text(text1);
                } else {
                    $('#valid-medios2').removeClass('text-danger');
                    $('#valid-medios2').text('');
                }
                break;
            case 16:
                if (String(valor).length < longitud) {
                    $('#valid-medios5').addClass('text-danger');
                    $('#valid-medios5').text(text1);
                } else {
                    $('#valid-medios5').removeClass('text-danger');
                    $('#valid-medios5').text('');
                }
                break;
            case 17:
                if (String(valor).length < longitud) {
                    $('#valid-medios7').addClass('text-danger');
                    $('#valid-medios7').text(text1);
                } else {
                    $('#valid-medios7').removeClass('text-danger');
                    $('#valid-medios7').text('');
                }
                break;
            case 18:
                if (String(valor).length < longitud) {
                    $('#valid-medios8').addClass('text-danger');
                    $('#valid-medios8').text(text1);
                }
                else if (String(valor).length > longitud) {
                    $('#valid-medios8').addClass('text-danger');
                    $('#valid-medios8').text(text2);
                } else {
                    $('#valid-medios8').removeClass('text-danger');
                    $('#valid-medios8').text('');
                }
                break;
        }
    }
    $scope.getCasasFinancierasActivas = function () {
        BayportService.getCasasFinancierasActivas().then(function (d) {
            $scope.casasFinancierasList = d.data;
        });
    }
    $scope.cadenaValida = function (valor, longitud) {
        var valid = true;
        console.log(valor)
        if (String(valor).length != longitud) {
            valid = false;
        }
        return valid;
    }
    $scope.cadenaValida1 = function (valor, longitud) {
        var valid = true;
        if (String(valor).length < longitud) {
            valid = false;
        }
        return valid;
    }
    $scope.validaSector = function (cod) {
        $scope.validSector = $scope.sectoresList.ListSector.filter(d => d.secuencia === cod);
    }
    $scope.cambioFormulario = function (formulario, formularioCargado) {
        var cambios = {};
        if (Object.keys(formulario).length === Object.keys(formularioCargado).length) {
            for (var key of Object.keys(formulario)) {
                if (key != 'msg' && key != 'expediente_completo') {
                    if (key == 'cartera') {
                        if (formulario[key].length != formularioCargado[key].length) {
                            cambios[key] = formulario[key];
                        }
                    }
                    else {
                        if (formulario[key] && formularioCargado[key]) {
                            if (formulario[key].toString() != formularioCargado[key].toString()) {
                                cambios[key] = formulario[key];
                            }
                        } else if (formulario[key] && !formularioCargado[key]) {
                            cambios[key] = formulario[key];
                        } else if (!formulario[key] && formularioCargado[key]) {
                            cambios[key] = formulario[key];
                        }
                    }
                }
            }
            return cambios;
        } else {
            cambios = formulario;
            return cambios;
        }
    }
});