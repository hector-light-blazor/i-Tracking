const body = document.querySelector("body")

const toast = {
    dom: null,
    DomTitle: null,
    DomBody: null,
    timeout: null,
    delay: null,
    "config": {
        "body": "#ToastBody",
        "title" : "#ToastTitle"
    },
    "main": {
        "id" : "#toast-1",
        "showing": "showing",
        "show": "show",
        "hide": "hide"
    }
}


const modal_back_drop = {
    "select": "#modal_back_drop",
    "id": "modal_back_drop",
    "class": "modal-backdrop fade show"
}

const body_info = {
    "style": "overflow: hidden; padding-right: 17px",
    "class": "modal-open"
}


window.ToastCounter = 0;



window.CreateToast = (container, idSequence, primary,title, message) => {

    const mainDiv = document.createElement("div");
    const ToastHeader = document.createElement("div");
    const ToastBody = document.createElement("div");
    const Itag = document.createElement("i");
    const strong = document.createElement("strong");
    const small = document.createElement("small");
    const button = document.createElement("button")

    mainDiv.className = "toast show showing mb-3";
    mainDiv.setAttribute("id", `toast-${idSequence}`)

    ToastHeader.setAttribute("class", `toast-header ${primary}`);
    Itag.setAttribute("class", "far fa-bell text-muted me-2");
    strong.setAttribute("class", "me-auto");

    ToastBody.setAttribute("class", "toast-body");
    button.setAttribute("role", "button")
    button.setAttribute("type", "button")
    button.setAttribute("class", "btn-close")

    strong.appendChild(document.createTextNode(title));
    ToastBody.appendChild(document.createTextNode(message));

    ToastHeader.appendChild(Itag)
    ToastHeader.appendChild(strong)
    ToastHeader.appendChild(small)
    ToastHeader.appendChild(button)

    mainDiv.appendChild(ToastHeader);
    mainDiv.appendChild(ToastBody);

    container.appendChild(mainDiv);

    

   

    const timer = setTimeout(() => {
        mainDiv.remove();
        clearTimeout(timer);
    }, 6000)

    const showingTimer = setTimeout(() => {
        mainDiv.classList.remove("showing");
        clearTimeout(showingTimer);
    }, 500)


    button.onclick = () => {
        mainDiv.remove();
        if (timer) {
            clearTimeout(timer)
        }
            button.onclick = null;
    }

}

window.ShowToast = (klass, title, body) => {
    window.CreateToast(document.querySelector("#toast"), window.ToastCounter++, klass, title, body)
}


window.ToastShow = (klass, title, body) => {
    if (toast.dom == null) {
        toast.dom = document.querySelector(toast.main.id)
        toast.DomBody = document.querySelector(toast.config.body);
        toast.DomTitle = document.querySelector(toast.config.title);
    }

    toast.DomBody.innerHTML = body;
    toast.DomTitle.innerHTML = title;

    toast.dom.classList.remove(toast.main.hide);
    toast.dom.classList.add(toast.main.show)
    toast.dom.classList.add(toast.main.showing)
    toast.timeout = setTimeout(() => {
        toast.dom.classList.remove(toast.main.showing);
        clearTimeout(toast.timeout)
    }, 500)


    toast.delay = setTimeout(() => {
        toast.dom.classList.remove(toast.main.show);
        toast.dom.classList.add(toast.main.hide);
        clearTimeout(toast.delay)
    }, 4500)
}

window.ToastRemove = () => {

    if (toast.dom ==  null) {
        toast.dom = document.querySelector(toast.main.id);
        toast.DomBody = document.querySelector(toast.config.body);
        toast.DomTitle = document.querySelector(toast.config.title);
    }
    clearTimeout(toast.delay);
    toast.dom.classList.add(toast.main.showing)
    toast.timeout = setTimeout(() => {
        toast.dom.classList.add(toast.main.hide)
        toast.dom.classList.remove(toast.main.showing)
        toast.dom.classList.remove(toast.main.show)
        clearTimeout(toast.timeout)
    }, 500);

}


window.addModal = () => {
    console.log("I WAS CALLED", new Date().toUTCString())

    var div = document.createElement("div")
    div.id = modal_back_drop.id;

    div.className = modal_back_drop.class


    body.style = body_info.style
    body.classList.add(body_info.class)

    body.appendChild(div)

}

window.removeModal = () => {
    console.log("I WAS CALLED", new Date().toUTCString())
    var div = document.querySelector(modal_back_drop.select)
    if (div !== null) {
        div.remove();

    }

    body.style = "";
    body.classList.remove(body_info.class);
}