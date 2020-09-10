import React from 'react';
import MaterialTable from 'material-table';

export default function Employees() {
  const [state, setState] = React.useState({
    columns: [
      {title: 'Name', field: 'name'},
      {title: 'Surname', field: 'surname'},
      {title: 'Birth Year', field: 'birthYear', type: 'numeric'},
      {
        title: 'Birth Country',
        field: 'birthcountry',
        lookup: {34: 'Australia', 63: 'Russia', 30: 'Nepal'},
      },
    ],
    data: [
      {name: 'Anton', surname: 'Polkanov', birthYear: 2020, birthcountry: 63},
      {
        name: 'Jason',
        surname: 'Phua',
        birthYear: 2019,
        birthcountry: 34,
      },
      {
        name: 'Narayan',
        surname: 'Pudasaini',
        birthYear: 1999,
        birthcountry: 30
      }
    ],
  });

  return (
    <MaterialTable
      title="Employee list"
      columns={state.columns}
      data={state.data}
      editable={{
        onRowAdd: (newData) =>
          new Promise((resolve) => {
            setTimeout(() => {
              resolve();
              setState((prevState) => {
                const data = [...prevState.data];
                data.push(newData);
                var d = new FormData();
                d.append("lastName", JSON.stringify(data));
                fetch('employeemanagement/add',
                  {
                    method: "POST",
                    body: d
                  })
                  .then(response => response.text())
                  .then(data => data)
                return {...prevState, data};
              });
            }, 600);
          }),
        onRowUpdate: (newData, oldData) =>
          new Promise((resolve) => {
            setTimeout(() => {
              resolve();
              if (oldData) {
                setState((prevState) => {
                  const data = [...prevState.data];
                  data[data.indexOf(oldData)] = newData;
                  return {...prevState, data};
                });
              }
            }, 600);
          }),
        onRowDelete: (oldData) =>
          new Promise((resolve) => {
            setTimeout(() => {
              resolve();
              setState((prevState) => {
                const data = [...prevState.data];
                data.splice(data.indexOf(oldData), 1);
                return {...prevState, data};
              });
            }, 600);
          }),
      }}
    />
  );
}
