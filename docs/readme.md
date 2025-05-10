# Forgetful Wizard project
## About
> experiment project, we'll see where this goes

## Importamt

put this somewhere:
```
The five boxing wizards jump quickly
```

## TO-DO
* [ ] simple scene
    * [ ] light weight scene to experiment in
    * [ ] basic movement
    * [ ] dash/blink
    * [ ] clones

## TileSet data

### details

| `ID` | `NAME ` | `ADJ. BITS  ` | `TILE DETAILS                   ` |
| ---- | ------- | ------------- | --------------------------------- |
| `00` | `Empty` | `000 000 000` | `empty tile                     ` |
| `01` | `C1_FL` | `100 000 000` | `corner front left              ` |
| `02` | `W1_F ` | `010 000 000` | `wall front                     ` |
| `03` | `C1_FR` | `001 000 000` | `corner front right             ` |
| `04` | `C2_AF` | `101 000 000` | `corners front                  ` |
| `05` | `W1_L ` | `000 100 000` | `wall left                      ` |
| `06` | `W2_FL` | `010 100 000` | `walls front left               ` |
| `07` | `Block` | `000 010 000` | `filled in tile                 ` |
| `08` | `W1_R ` | `000 001 000` | `wall right                     ` |
| `09` | `W2_FR` | `010 001 000` | `walls front right              ` |
| `10` | `W2_PF` | `000 101 000` | `walls parallel front open      ` |
| `11` | `W3_NB` | `010 101 000` | `walls all but back             ` |
| `12` | `C1_BL` | `000 000 100` | `corner back left               ` |
| `13` | `C2_AL` | `100 000 100` | `corners left                   ` |
| `14` | `C2_DR` | `001 000 100` | `corners diagonal front right   ` |
| `15` | `C3_FL` | `101 000 100` | `corners adjacent to front left ` |
| `16` | `W1_B ` | `000 000 010` | `wall back                      ` |
| `17` | `W2_PL` | `010 000 010` | `walls parallel left open       ` |
| `18` | `W2_BL` | `000 100 010` | `walls back left                ` |
| `19` | `W3_NR` | `010 100 010` | `walls all but right            ` |
| `20` | `W2_BR` | `000 001 010` | `walls back right               ` |
| `21` | `W3_NL` | `010 001 010` | `walls all but left             ` |
| `22` | `W3_NF` | `000 101 010` | `walls all but front            ` |
| `23` | `W4   ` | `010 010 010` | `walls all                      ` |
| `24` | `C1_BR` | `000 000 001` | `corner back right              ` |
| `25` | `C2_DL` | `100 000 001` | `corners diagonal front left    ` |
| `26` | `C2_AR` | `001 000 001` | `corners right                  ` |
| `27` | `C3_FR` | `101 000 001` | `corners adjacent to front right` |
| `28` | `C2_AB` | `000 000 101` | `corners back                   ` |
| `29` | `C3_BL` | `100 000 101` | `corners adjacent to back left  ` |
| `30` | `C3_BR` | `001 000 101` | `corners adjacent to back right ` |
| `31` | `C4   ` | `101 000 101` | `corner                         ` |

### alphabetical ordering

| `ID` | `NAME   ` | `row` | `col` | `ADJ. BITS  ` | `VACANT BITS` | 
| ---- | --------- | ----- | ----- | ------------- | ------------- | 
| `00` | `Block  ` | ` 0 ` | ` 0 ` | `000 010 000` | `000 000 000` | 
| `01` | `C1_BL  ` | ` 0 ` | ` 1 ` | `000 000 100` | `111 111 011` | 
| `02` | `C1_BR  ` | ` 0 ` | ` 2 ` | `000 000 001` | `111 111 110` | 
| `03` | `C1_FL  ` | ` 0 ` | ` 3 ` | `100 000 000` | `011 111 111` | 
| `04` | `C1_FR  ` | ` 0 ` | ` 4 ` | `001 000 000` | `110 111 111` | 
| `05` | `C2_AB  ` | ` 0 ` | ` 5 ` | `000 000 101` | `111 111 010` | 
| `06` | `C2_AF  ` | ` 0 ` | ` 6 ` | `101 000 000` | `010 111 111` | 
| `07` | `C2_AL  ` | ` 0 ` | ` 7 ` | `100 000 100` | `011 111 011` | 
| `08` | `C2_AR  ` | ` 1 ` | ` 0 ` | `001 000 001` | `110 111 110` | 
| `09` | `C2_DL  ` | ` 1 ` | ` 1 ` | `100 000 001` | `011 111 110` | 
| `10` | `C2_DR  ` | ` 1 ` | ` 2 ` | `001 000 100` | `110 111 011` | 
| `11` | `C3_BL  ` | ` 1 ` | ` 3 ` | `100 000 101` | `011 111 010` | 
| `12` | `C3_BR  ` | ` 1 ` | ` 4 ` | `001 000 101` | `110 111 010` | 
| `13` | `C3_FL  ` | ` 1 ` | ` 5 ` | `101 000 100` | `010 111 011` | 
| `14` | `C3_FR  ` | ` 1 ` | ` 6 ` | `101 000 001` | `010 111 110` | 
| `15` | `C4     ` | ` 1 ` | ` 7 ` | `101 000 101` | `010 111 010` | 
| `16` | `Empty  ` | ` 2 ` | ` 0 ` | `000 000 000` | `111 111 111` | 
| `17` | `W1_B   ` | ` 2 ` | ` 1 ` | `000 000 010` | `111 111 000` | 
| `18` | `W1_F   ` | ` 2 ` | ` 2 ` | `010 000 000` | `000 111 111` | 
| `19` | `W1_L   ` | ` 2 ` | ` 3 ` | `000 100 000` | `011 011 011` | 
| `20` | `W1_R   ` | ` 2 ` | ` 4 ` | `000 001 000` | `110 110 110` | 
| `21` | `W2_BL  ` | ` 2 ` | ` 5 ` | `000 100 010` | `011 011 000` | 
| `22` | `W2_BR  ` | ` 2 ` | ` 6 ` | `000 001 010` | `110 110 000` | 
| `23` | `W2_FL  ` | ` 2 ` | ` 7 ` | `010 100 000` | `000 011 011` | 
| `24` | `W2_FR  ` | ` 3 ` | ` 0 ` | `010 001 000` | `000 110 110` | 
| `25` | `W2_PF  ` | ` 3 ` | ` 1 ` | `000 101 000` | `010 010 010` | 
| `26` | `W2_PL  ` | ` 3 ` | ` 2 ` | `010 000 010` | `000 111 000` | 
| `27` | `W3_NB  ` | ` 3 ` | ` 3 ` | `010 101 000` | `000 010 010` | 
| `28` | `W3_NF  ` | ` 3 ` | ` 4 ` | `000 101 010` | `010 010 000` | 
| `29` | `W3_NL  ` | ` 3 ` | ` 5 ` | `010 001 010` | `000 110 000` | 
| `30` | `W3_NR  ` | ` 3 ` | ` 6 ` | `010 100 010` | `000 011 000` | 
| `31` | `W4     ` | ` 3 ` | ` 7 ` | `010 010 010` | `000 010 000` | 

### logging

* `TileSetData.cs`
```c#
    private bool adjacencyHashPrepared;
    private int adjacencyHash;
    private bool vacancyHashPrepared;
    private int vacancyHash;

    public int GetAdjacencyHash(){
        // already have?
        if(this.adjacencyHashPrepared){
            return this.adjacencyHash;
        }
        // calculate it
        else {
            this.adjacencyHash = GetAdjacencyHashFrom(this.filledRequired);
            this.adjacencyHashPrepared = true;
            // then give it
            return this.adjacencyHash;
        }
    }
    public int GetVacancyHash(){
        // already have?
        if(this.vacancyHashPrepared){
            return this.vacancyHash;
        }
        // calculate it
        else {
            this.vacancyHash = GetAdjacencyHashFrom(this.vacancyRequired);
            this.vacancyHashPrepared = true;
            // then give it
            return this.vacancyHash;
        }
    }

    public static int GetAdjacencyHashFrom(bool[] adjacentFilled){
        int result = 0;
        for(int i = 0; i < adjacentFilled.Length; i++){
            // add left shifted bit based on adjacency
            //  index is the power of 2 to add
            if(adjacentFilled[i]){
                result += (0x01 << i);
            }
        }
        // then give it
        return result;
    }
    public void ListTileData(){
        for(int i = 0; i < tileDataList.Length; i++){
            int tileAdjHash = GetAdjacencyHash(tileDataList[i].filledRequired);
            Debug.Log( "tileDataList["+(i)+"]: '"+(tileDataList[i].name)+"', ["+(tileAdjHash)+"]" );
        }
    }

    public void LogHashOrderStatus(){
        // prepare hash list
        int[] tileHashList = new int[tileDataList.Length];
        // fill hash list
        for(int i = 0; i < tileDataList.Length; i++){
            tileHashList[i] = GetAdjacencyHash(tileDataList[i].filledRequired);
        }
        // now check each if it's less than previous
        int searchIndex = 1;
        for(; searchIndex < tileDataList.Length; searchIndex++){
            if(tileHashList[searchIndex] < tileHashList[searchIndex-1]){
                Debug.Log("tileDataList["+(searchIndex)+"] NEEDS EARLIER");
                break;
            }
        }
        if(searchIndex >= tileDataList.Length){
            Debug.Log("tileDataList in hash order :D");
        }
    }
```

* `TileMap3D.cs`
```c#
    public TileSetData tileset;
    public float debugLoggingSnoozeTime = 0.5f;
    public float debugLoggingSnooze = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.tileset.ListTileData();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.debugLoggingSnooze == 0.0f){
            if(Input.GetKey(KeyCode.P)){
                this.tileset.ListTileData();
                this.debugLoggingSnooze = this.debugLoggingSnoozeTime;
            }
            if(Input.GetKey(KeyCode.O)){
                this.tileset.LogHashOrderStatus();
                this.debugLoggingSnooze = this.debugLoggingSnoozeTime;
            }
        }
        else {
            this.debugLoggingSnooze = Mathf.Max(0.0f, this.debugLoggingSnooze - Time.deltaTime);
        }
    }
```

## references

### normal map generation
* github project to generate normal maps in web page [https://cpetry.github.io/NormalMap-Online/](https://cpetry.github.io/NormalMap-Online/)