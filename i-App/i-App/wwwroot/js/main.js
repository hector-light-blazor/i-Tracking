window.animate = document.getElementById("loading");
window.Acontainer = document.getElementById("loading-container");
window.running = false;
window.stopid;
window.increase = -200;
window.ratio = 1;



window.SelDeptChange = () => {
    //Change the position of the representatives
    //@page {Add Vehicle}
    const cntRep = document.getElementById(window.AddVehicle.FormControlRep.id)
    cntRep.selectedIndex = 0;
}


window.AddVehicle = {
    FormControlDept: {
        id: "FormControlDept",
        select: null
    },
    FormControlRep: {
        id: "FormControlRep"
    }
}

window.addVehicleSetup = () => {

    if (window.AddVehicle.FormControlDept.select) {
        window.AddVehicle.FormControlDept.select.removeEventListener("change", window.SelDeptChange);
    }
   


    setTimeout(() => {
        window.AddVehicle.FormControlDept.select = document.getElementById(window.AddVehicle.FormControlDept.id);
        

        //console.log(window.AddVehicle.FormControlDept.select, window.AddVehicle.FormControlDept.id);
        //console.log(window.AddVehicle.FormControlLocation.select, window.AddVehicle.FormControlLocation.id)

        //Add Event Listener...
        if (window.AddVehicle.FormControlDept.select) {
            window.AddVehicle.FormControlDept.select.addEventListener("change", window.SelDeptChange);
        }
    }, 1000)
}

window.OnReady = () => {
    if (window.running) {
        window.stopAnimate();
    }
    window.animate = document.getElementById("loading");
    window.Acontainer = document.getElementById("loading-container");
    
    window.Acontainer.classList.remove("pace-inactive")
    window.Acontainer.classList.add("pace-progress");
    window.increase = window.increase + window.ratio;

    console.log("ON READY LOADING", window.increase);
    window.animate.style.width = `${window.increase}%`;
    window.running = true;
    startAnimate();
}

window.startAnimate = () => {
    window.animate.style.width = `${window.increase}%`;
    window.increase = window.increase + window.ratio;
    console.log("ANIMATING LOADING", window.increase);
    window.stopid = window.requestAnimationFrame(window.OnReady);
    if (window.increase >= 100) {
        window.stopAnimate();
    }

}

window.stopAnimate = () => {
    window.running = false;
    cancelAnimationFrame(window.stopid);
    window.Acontainer.classList.remove("pace-progress")
    window.Acontainer.classList.add("pace-inactive")
}