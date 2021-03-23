Myapp
    .controller('Admin_Controller', ['$scope', '$rootScope', '$state', 'CandidateService', 'CandidatePdfService', function ($scope, $rootScope, $state, Cs, CPs) {

        $scope.Applicant_dropDown = "All";
        $scope.AllApplicants = null;
        $scope.applicants = [];
        $scope.AllPDFs = null;
        $scope.search = "";
        
        $scope.openPdfInNewWindow = function (name, Base64String) {
            var obj = document.createElement('object');
            obj.style.width = '100%';
            obj.style.height = '842pt';
            obj.id = 'pdf';
            obj.type = 'application/pdf';
            obj.data = 'data:application/pdf;base64,' + Base64String;
            var pdfWindow = window.open('', name, "height=640,width=1260,toolbar=no,menubar=no,scrollbars=no,location=no,status=no");
            pdfWindow.document.body.appendChild(obj)
        }

        $scope.CheqAdminLogin = function () {
            if (!$rootScope.login)
                $state.go('AdminLogin');
        }

        $scope.cradantials = {
            username: null,
            password: null
        }

        $scope.AdminLogout = function () {
            $rootScope.login = null;
        }

        $scope.AdminLogin = function () {
            if ($scope.cradantials.password == 'admin') {
                Cs.GetCandidateByEmail($scope.cradantials.username)
                    .then(function (result) {
                        if (result && result.IsAdmin) {
                            $rootScope.login = result;
                            $state.go('Admin.Home');
                        }
                        else alert('Invalid Cradantials');
                    })
                    .catch(function (error) {
                        console.log('Error :');
                        console.log(error);
                    })
            }
            else {
                alert('Invalid Cradantials')
            }
        }

        $scope.FetchAllCandidate = function () {
            Cs.getAllCandidate()
                .then(function (result) {
                    $scope.AllApplicants = result;
                    $scope.SetApplicants();
                })
        }

        $scope.FetchAllPdfs = function () {
            CPs.getAllPDFs()
                .then(function (result) {
                    $scope.AllPDFs = result;
                })
        }

        $scope.SetApplicants = function () {

            if ($scope.Applicant_dropDown == "All") {
                $scope.applicants = $scope.AllApplicants
            }
            else if ($scope.Applicant_dropDown == "Shortlisted") {
                $scope.applicants = [];
                $scope.AllApplicants.forEach(function (item, index) {
                    if (item.IsShortListed) $scope.applicants.push(item);
                })
            }
            else if ($scope.Applicant_dropDown == "Not-shortlisted") {
                $scope.applicants = [];
                $scope.AllApplicants.forEach(function (item, index) {
                    if (!item.IsShortListed) $scope.applicants.push(item);
                })
            }
        }

        $scope.ChangeShortlisted = function (candidate) {
            Cs.saveNewCandidate(candidate)
                .catch(function (error) {
                    alert("Error Occured while change shortlisted status change Please refresh page");
                    console.log(Error);
                })
        }

        $scope.GetCandidatePdf = function (id) {
            CPs.GetPdfByCandidateId(id)
                .then(function (result) {
                    $scope.openPdfInNewWindow(result.PdfName, result.PdfData);
                })
        }

        $scope.searchPdf = function () {
            if ($scope.search) {
                Cs.getAllCandidate()
                    .then(function (result) {
                        $scope.AllApplicants = result;
                        return CPs.searchPdf($scope.search);
                    })
                    .then(function (result) {
                        $scope.temp = $scope.AllApplicants;
                        $scope.AllApplicants = [];
                        result.forEach(function (id, index) {
                            $scope.temp.forEach(function (candidate, index) {
                                if (candidate.Id == id) {
                                    $scope.AllApplicants.push(candidate);
                                }
                            })
                        })

                        $scope.SetApplicants();
                    })
            }
            else {
                $scope.FetchAllPdfs();
            }
        }

        $scope.FetchAllCandidate();
    }]);

//kendogrid