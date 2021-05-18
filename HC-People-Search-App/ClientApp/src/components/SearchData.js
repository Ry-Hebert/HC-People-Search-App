import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';

const useStyles = makeStyles((theme) => ({
    searchField: {
        width: '100%'
    }
}));

// export class SearchData extends Component {
//   static displayName = SearchData.name;

const SearchData = () => {
    const classes = useStyles()
    // Setup of state to handle People Data Results
    const [peopleData, setPeopleData] = useState({ dataRes: [null], loading: true })

    const populatePeopleData = async () => {
        const response = await fetch('weatherforecast')
        const data = await response.json()
        setPeopleData({ dataRes: data, loading: false })
    }

    useEffect(()=>{populatePeopleData()}, [1])

    const renderForecastsTable = (forecasts) => {
        return (
          <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
              <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
              </tr>
            </thead>
            <tbody>
              {forecasts.map(forecast =>
                <tr key={forecast.date}>
                  <td>{forecast.date}</td>
                  <td>{forecast.temperatureC}</td>
                  <td>{forecast.temperatureF}</td>
                  <td>{forecast.summary}</td>
                </tr>
              )}
            </tbody>
          </table>
        );
      }


    console.log(peopleData)
    let contents = peopleData.loading ? <p><em>Loading...</em></p> : renderForecastsTable(peopleData.dataRes);

    return (
        <div>
            <TextField className={classes.searchField} id='hcSearchField' label='Search' variant='outlined'></TextField>
            {contents}
        </div>
    );
}

export default SearchData