const getCellValue = (tr, idx) => tr.children[idx].innerText || tr.children[idx].textContent;

const comparer = (idx, asc) => (a, b) => ((v1, v2) =>
    v1 !== '' && v2 !== '' && !isNaN(v1) && !isNaN(v2) ? v1 - v2 : v1.toString().localeCompare(v2)
)(getCellValue(asc ? a : b, idx), getCellValue(asc ? b : a, idx));

headers = document.querySelectorAll('th');
headers.forEach(th => th.state = 'undefined');

headers.forEach(th => th.addEventListener('click', (() => {
    const table = th.closest('table');
    arr = Array.from(table.querySelectorAll('tr:nth-child(n+2)'));
    console.log(arr);
    arr.sort(comparer(Array.from(th.parentNode.children).indexOf(th), this.asc = !this.asc))
        .forEach(tr => table.appendChild(tr));
})));