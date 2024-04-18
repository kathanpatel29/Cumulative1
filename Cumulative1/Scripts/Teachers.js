// AJAX for adding a new teacher
function AddTeacher() {

    // Goal: send a request to create a new teacher
    // POST: /Teacher/Create
    // with POST data of TeacherFname, TeacherLname, EmployeeNumber, HireDate, Salary

    var URL = "/Teacher/Create";

    var rq = new XMLHttpRequest();
    // Define a new XMLHttpRequest object

    // Retrieve values from form fields
    var TeacherFname = document.getElementById('TeacherFname').value;
    var TeacherLname = document.getElementById('TeacherLname').value;
    var EmployeeNumber = document.getElementById('EmployeeNumber').value;
    var HireDate = document.getElementById('HireDate').value;
    var Salary = document.getElementById('Salary').value;

    // Prepare data object
    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname,
        "EmployeeNumber": EmployeeNumber,
        "HireDate": HireDate,
        "Salary": Salary
    };

    rq.open("POST", URL, true);
    // Initialize the request
    rq.setRequestHeader("Content-Type", "application/json");
    // Set the content type header for the request

    rq.onreadystatechange = function () {
        // Define a function to handle the response
        if (rq.readyState == 4 && rq.status == 200) {
            // Check if the request is complete and successful
            // Log the TeacherData object to console
            console.log(TeacherData);
            // No further action needed for now
        }
    };

    // Send the request with the serialized JSON data
    rq.send(JSON.stringify(TeacherData));
}


// AJAX for updating an existing teacher
function UpdateTeacher(teacherId) {
    // Goal: send a request to update an existing teacher
    // POST: /Teacher/Update/{teacherId}
    // with POST data of TeacherFname, TeacherLname, EmployeeNumber, HireDate, Salary

    var URL = "/Teacher/Update/" + teacherId;

    var rq = new XMLHttpRequest();
    // Define a new XMLHttpRequest object

    // Retrieve values from form fields
    var TeacherFname = document.getElementById('TeacherFname').value;
    var TeacherLname = document.getElementById('TeacherLname').value;
    var EmployeeNumber = document.getElementById('EmployeeNumber').value;
    var HireDate = document.getElementById('HireDate').value;
    var Salary = document.getElementById('Salary').value;

    // Prepare data object
    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname,
        "EmployeeNumber": EmployeeNumber,
        "HireDate": HireDate,
        "Salary": Salary
    };

    rq.open("POST", URL, true);
    // Initialize the request
    rq.setRequestHeader("Content-Type", "application/json");
    // Set the content type header for the request

    rq.onreadystatechange = function () {
        // Define a function to handle the response
        if (rq.readyState == 4 && rq.status == 200) {
            // Check if the request is complete and successful
            // Log the TeacherData object to console
            console.log(TeacherData);
            // No further action needed for now
        }
    };

    // Send the request with the serialized JSON data
    rq.send(JSON.stringify(TeacherData));
}