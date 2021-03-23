Myapp
    .controller('Application_Form_Controller', ['$scope', '$state', '$stateParams', 'CandidateService', 'CandidatePdfService', function ($scope, $state, $stateParams, CandidateService, CandidatePdfService) {

        $scope.candidate = null;
        $scope.candidatePdf = null;
        $scope.candidateUri = null;
        var pdf = document.getElementById('Resume');
        fr = new FileReader();

        if (document.getElementById('pdf')) {
            document.getElementById('pdf').hidden = true;
        }

        if (document.getElementById('CandidateInfo')) {
            document.getElementById('CandidateInfo').hidden = true;
        }

        function varifyCandidate(result) {
            var ContactNo = document.getElementById('ContactNo').value;
            var SkypeId = document.getElementById('SkypeId').value;
            console.log()
            if (ContactNo == result.ContactNo && SkypeId == result.SkypeId) return true;
            return false;
        }

        $scope.handleFileSelect = function () {

            if (!window.File || !window.FileReader || !window.FileList || !window.Blob) {
                alert('The File APIs are not fully supported in this browser.');
                return;
            }

            var input = document.getElementById('Resume');
            if (!input) {
                alert("Um, couldn't find the fileinput element.");
            }
            else if (!input.files) {
                alert("This browser doesn't seem to support the `files` property of file inputs.");
            }
            else if (!input.files[0]) {
                alert("Please select a file before clicking 'Load'");
            }
            else {
                var file = pdf.files[0];
                $scope.candidatePdf.PdfName = file.name;
                fr.onload = receivedText;
                //fr.readAsText(file);
                //fr.readAsBinaryString(file); //as bit work with base64 for example upload to server
                fr.readAsDataURL(file);
            }
        }

        var receivedText = function () {
            $scope.candidatePdf.PdfData = fr.result.slice(28);
            $scope.Submit_Application_form();
        }

        $scope.ConvertBase64ToPdf = function (Base64String) {
            var obj = document.createElement('object');
            obj.style.width = '100%';
            obj.style.height = '842pt';
            obj.id = 'pdf';
            obj.type = 'application/pdf';
            obj.data = 'data:application/pdf;base64,' + Base64String;
            document.getElementById('CandidatePdf').appendChild(obj);
            document.getElementById('SerachBar').hidden = true;
        }

        if ($stateParams.id) {
            CandidateService
                .getCandidateById($stateParams.id)
                .then(function (result) {
                    $scope.candidate = result;
                })
        }
        else {
            CandidateService
                .getNewCandidate()
                .then(
                    function successCallBack(Responce) {
                        $scope.candidate = Responce;
                    },
                    function errorCallBack(Error) {
                        console.log("Error" + Error);
                    }
                );
        }
        

        CandidatePdfService
            .GetNewPdf()
            .then(
                function successCallBack(Responce) {
                    $scope.candidatePdf = Responce;
                },
                function errorCallBack(Error) {
                    console.log("Error" + Error);
                }
            );


        $scope.Submit_Application_form = function () {
            // save candidate
            CandidateService.saveNewCandidate($scope.candidate)
                .then(function (result /* rsult => candidateUri*/) {
                    // fetch same candidate by uri
                    console.log(result);
                    return CandidateService.getCandidateByUri(result)
                })
                .then(function (result /*result => candidateObject*/) {
                    // set pdf candidate as responce
                    console.log(result);
                    $scope.candidatePdf.CandidateId = result
                    // save pdf object
                    return CandidatePdfService.SaveCandidatePdf($scope.candidatePdf)
                })
                .then(function (responce) {
                    alert("Application Submitted successfully")
                })
                .catch(function (error) {
                    console.log(error);
                    alert("Some Error occured: "  + error)
                })
        }

        $scope.Search_application_form = function (Email) {
            //Find candidate by email
            CandidateService.GetCandidateByEmail(Email)
                .then(function (result /* result => candidateObject*/) {
                    //find pdf by candidate Id
                    if (result && varifyCandidate(result)) {
                        $scope.candidate = result
                        return CandidatePdfService.GetPdfByCandidateId(result.Id)
                    }
                    else {
                        alert("No Candidate is available with given Info:")
                    }
                })
                .then(function (result /* result => candidatePdf */) {
                    //convert base64 to pdf
                    $scope.candidatePdf = result
                    $scope.ConvertBase64ToPdf(result.PdfData);
                    document.getElementById('CandidateInfo').hidden = false;
                })
                .catch(function (error) {
                    console.log('Error :');
                    console.log(error);
                    alert("Probleam occured while fatching your data");
                })
        }

        $scope.DeleteCandidate = function () {
            console.log($scope.candidatePdf);
            CandidatePdfService
                .DeleteCandidatePdf($scope.candidate.Id)
                .then(function (result) {
                    console.log(result);
                    console.log($scope.candidatePdf.Id);
                    return CandidateService.DeleteCandidate($scope.candidate.Id)
                })
                .then(function (result) {
                    console.log(result);
                    alert("Application removed Successfully");
                    $state.go('Home');
                })
                .catch(function (error) {
                    console.log('Error :');
                    console.log(error);
                    alert("Probleam occured while removing your application");
                })
        }
}]);

