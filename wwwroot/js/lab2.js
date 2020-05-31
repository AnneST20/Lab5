const uri = 'api/Products';
let firms = [];

function getFirms() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayFirms(data))
        .catch(error => console.error('Unable to get prodcts.', error));
}

function addFirm() {
    const addFirmTextbox = document.getElementById('add-firm');
    const addCosmeticsTextbox = document.getElementById('add-cosmetics');
    const addPriceTextbox = document.getElementById('add-cosmetics');

    const product = {
        name: addFirmTextbox.value.trim(),
        info: addCosmeticsTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })
        .then(response => response.json())
        .then(() => {
            getFirms();
            addFirmTextbox.value = '';
            addCosmeticsTextbox.value = '';
        })
        .catch(error => console.error('Unable to add product.', error));
}

function deleteFirm(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getFirms())
        .catch(error => console.error('Unable to delete product.', error));
}

function displayEditForm(id) {
    const product = firms.find(product => product.id === id);

    document.getElementById('edit-id').value = product.id;
    document.getElementById('edit-name').value = product.name;
    document.getElementById('edit-area').value = product.area;
    document.getElementById('editForm').style.display = 'block';
}

function updateFirm() {
    const productId = document.getElementById('edit-id').value;
    const product = {
        id: parseInt(productId, 10),
        name: document.getElementById('edit-name').value.trim(),
        info: document.getElementById('edit-area').value.trim()
    };

    fetch(`${uri}/${productId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })
        .then(() => getFirms())
        .catch(error => console.error('Unable to update product.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayFirms(data) {
    const tBody = document.getElementById('products');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(product => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${product.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteProduct(${product.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(product.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(product.area);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    products = data;
}
