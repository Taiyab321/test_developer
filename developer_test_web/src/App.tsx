import React, { useState } from 'react';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import PostCode from "./interfeces/postCode";
import './App.css';
import { any } from 'prop-types';
import postcode from './interfeces/postCode';

function App() {

    const postCodeData: postcode[] = [];
    const [items, setItems] = useState([]);
    const [rowData, setRowData] = useState(postCodeData);

    const onSearchPostCode = (event: any, values: any) => {
        console.log(process.env.REACT_APP_BASE_API_URL);
        setItems([])
        fetch(process.env.REACT_APP_BASE_API_URL + values)
            .then(res => res.json())
            .then(
                (result) => {
                    setItems(result.result == null ? [] : result.result);
                },
                (error) => {

                }
            )
    }

    const onSelectPostCode = (event: any, values: any) => {
        fetch(process.env.REACT_APP_BASE_API_URL + "LookupPostcode/" + values)
            .then(res => res.json())
            .then(
                (result) => {
                    postCodeData.push(result.result);
                    setRowData(postCodeData);
                },
                (error) => {

                }
            )
    }
  return (
    <div className="App">
      <header className="App-header"></header>
          <form className="container">
              <div className="childContainer">
                  <Autocomplete className="displayed"
                          disablePortal
                          id="combo-box-demo"
                          options={items}
                          onInputChange={onSearchPostCode}
                          onChange={onSelectPostCode}
                      sx={{ width: 300 }}
                      renderInput={(params) => <TextField {...params} label="select" />}
                      />
              
                  <div className="PostCode-Overview">
                      <table>
                          <tbody>
                              <tr className="Table-Header">
                                  <td>
                                      <h4>Country</h4>
                                  </td>
                                  <td>
                                      <h4>Region</h4>
                                  </td>
                                  <td>
                                      <h4>Admin District</h4>
                                  </td>
                                  <td>
                                      <h4>Parliamentary constituency</h4>
                                  </td>
                                  <td>
                                      <h4>Area</h4>
                                  </td>

                              </tr>
                              { rowData.map((listValue, index) => {
                                  if (rowData.length != 0) {
                                      return (
                                          <tr key={index}>
                                              <td>{listValue.country}</td>
                                              <td>{listValue.region}</td>
                                              <td>{listValue.admin_district}</td>
                                              <td>{listValue.parliamentary_constituency}</td>
                                              <td>{listValue.area}</td>
                                          </tr>
                                      );
                                  }
                              })}
                            </tbody>
                      </table>
                  </div>
              </div>
             

          </form>
          
    </div>
  );
}

export default App;
