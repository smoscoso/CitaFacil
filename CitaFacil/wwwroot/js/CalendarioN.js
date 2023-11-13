// Calendario.js
const daysTag = document.querySelector(".days");
const currentDate = document.querySelector(".current-date");
const prevNextIcon = document.querySelectorAll(".icons span");
const selectedDates = [];

// getting new date, current year and month
let date = new Date();
let currYear = date.getFullYear();
let currMonth = date.getMonth();

// storing full name of all months in an array
const months = ["January", "February", "March", "April", "May", "June", "July",
    "August", "September", "October", "November", "December"];

const renderCalendar = () => {
    let firstDayofMonth = new Date(currYear, currMonth, 1).getDay(); // getting first day of the month
    let lastDateofMonth = new Date(currYear, currMonth + 1, 0).getDate(); // getting last date of the month
    let lastDayofMonth = new Date(currYear, currMonth, lastDateofMonth).getDay(); // getting last day of the month
    let lastDateofLastMonth = new Date(currYear, currMonth, 0).getDate(); // getting last date of the previous month

    let liTag = "";

    for (let i = firstDayofMonth; i > 0; i--) { // creating li for previous month's last days
        liTag += `<li class="inactive">${lastDateofLastMonth - i + 1}</li>`;
    }

    for (let i = 1; i <= lastDateofMonth; i++) { // creating li for all days of the current month
        // adding active class to li if the current day, month, and year match
        let isToday = i === date.getDate() && currMonth === new Date().getMonth()
            && currYear === new Date().getFullYear() ? "active" : "";
        liTag += `<li class="${isToday}">${i}</li>`;
    }

    for (let i = lastDayofMonth; i < 6; i++) { // creating li for the first days of the next month
        liTag += `<li class="inactive">${i - lastDayofMonth + 1}</li>`;
    }

    currentDate.innerText = `${months[currMonth]} ${currYear}`; // setting current month and year as currentDate text
    daysTag.innerHTML = liTag;

    // Adding click event listeners to each day
    const dayElements = daysTag.querySelectorAll("li");
    dayElements.forEach(dayElement => {
        dayElement.addEventListener("click", () => {
            const selectedDay = parseInt(dayElement.textContent);
            const selectedDate = new Date(currYear, currMonth, selectedDay);

            if (selectedDate > date) {

                AddCalendar(selectedDate.toISOString());
                //selectedDates.push(selectedDate); // Save selected future date to the array
                //updateSelectedDatesList(); // Update the displayed list of selected dates

                //// Update the hidden input field with the selected date
                //const selectedDateInput = document.getElementById("selectedDate");
                //selectedDateInput.value = selectedDate.toISOString(); // Store the date in ISO format


            } else {
                alert("No se pueden escoger citas para este día.");
            }
        });
    });
};

function AddCalendar(selectedDate) {

    var fechaModel = { fecha: selectedDate };

    $.ajax({
        type: "POST", // la variable type guarda el tipo de la peticion GET,POST,..
        url: "CalendarioN", //url guarda la ruta hacia donde se hace la peticion
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(fechaModel),
        success: function (response) { //success es una funcion que se utiliza si el servidor retorna informacion
            
            alert(`La cita fue asignada para el día ${new Date(selectedDate).toDateString()}`);
            window.location = response.url + "?fecha=" + encodeURIComponent(selectedDate);;
            
        }
    });
}

function updateSelectedDatesList() {
    const selectedDatesList = document.querySelector(".selected-dates-list");
    selectedDatesList.innerHTML = ""; // Clear the list

    selectedDates.forEach(date => {
        const listItem = document.createElement("li");
        listItem.textContent = date.toDateString();
        selectedDatesList.appendChild(listItem);
    });
}

renderCalendar();

prevNextIcon.forEach(icon => { // getting prev and next icons
    icon.addEventListener("click", () => { // adding click event on both icons
        // if the clicked icon is the previous icon then decrement the current month by 1 else increment it by 1
        currMonth = icon.id === "prev" ? currMonth - 1 : currMonth + 1;
        if (currMonth < 0 || currMonth > 11) { // if the current month is less than 0 or greater than 11
            // creating a new date of the current year & month and passing it as the date value
            date = new Date(currYear, currMonth, new Date().getDate());
            currYear = date.getFullYear(); // updating the current year with the new date's year
            currMonth = date.getMonth(); // updating the current month with the new date's month
        } else {
            date = new Date(); // pass the current date as the date value
        }
        renderCalendar(); // calling renderCalendar function
    });
});
