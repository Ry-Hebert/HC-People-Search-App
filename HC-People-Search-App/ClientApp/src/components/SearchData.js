import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import Avatar from '@material-ui/core/Avatar';

const useStyles = makeStyles((theme) => ({
    searchField: {
        width: '100%'
    },
    root: {
        width: '100%',
        backgroundColor: theme.palette.background.paper,
    },
    secondaryInfo: {
        display: 'grid',
        justifyContent: 'end',
        textAlign: 'right',
    },
    searchContent: {
        width:  '100%',
    },
}));

// export class SearchData extends Component {
//   static displayName = SearchData.name;

const SearchData = () => {
    const classes = useStyles()
    // Setup of state to handle People Data Results
    const [peopleData, setPeopleData] = useState({ dataRes: [null], loading: true })

    const populatePeopleData = async () => {
        const response = await fetch('peoplesearch')
        const data = await response.json()
        setPeopleData({ dataRes: data, loading: false })
    }

    useEffect(()=>{populatePeopleData()}, [1])

    const renderPeopleListTable = (peopleList) => {
        return (
          <List className={classes.root}>
              {peopleList.map(people =>
                <ListItem key={people.name} divider='true'>
                  <ListItemAvatar>
                      <Avatar src={people.image} variant='circle' />
                  </ListItemAvatar>
                  <ListItemText primary={people.name} secondary={people.address} secondaryTypographyProps={{ noWrap: false }}></ListItemText>
                  <ListItemText className={classes.secondaryInfo} primary={"Age: " + people.age} secondary={people.interests}></ListItemText>
                </ListItem>
              )}
          </List>
        );
      }


    console.log(peopleData)
    let contents = peopleData.loading ? <p><em>Loading...</em></p> : renderPeopleListTable(peopleData.dataRes);

    return (
        <div className={classes.searchContent}>
            <TextField className={classes.searchField} id='hcSearchField' label='Search' variant='outlined'></TextField>
            {contents}
        </div>
    );
}

export default SearchData