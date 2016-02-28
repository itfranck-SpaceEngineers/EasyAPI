/************************************************************************************
EasyAPI - Documentation: http://steamcommunity.com/sharedfiles/filedetails/?id=381043
*************************************************************************************/

public class Example : EasyAPI
{
    public Example(IMyGridTerminalSystem grid, IMyProgrammableBlock me, Action<string> echo, TimeSpan elapsedTime) : base(grid, me, echo, elapsedTime)
    {
        // Start your code here
    }
}


/*********************************************/ 
/*** Advanced users only beyond this point ***/ 
/*********************************************/ 

Example state;

void Main(string argument)
{
    if(state == null)
    {
        state = new Example(GridTerminalSystem, Me, Echo, ElapsedTime);
    }

    // Set the minimum time between ticks here to prevent lag.
    // To utilise onSingleTap and onDoubleTap, set the minimum time to the same
    // time period of the timer running this script (e.g. 1 * EasyAPI.Seconds).
    state.Tick(100 * EasyAPI.Milliseconds, argument);
}


/*** Ignore minified library code below ***/ 
public abstract class EasyAPI{private long d=0;private long f=0;private long g=0;public EasyBlock Self;public IMyGridTerminalSystem GridTerminalSystem;public Action<string>Echo;public TimeSpan ElapsedTime;static public IMyGridTerminalSystem grid;private Dictionary<string,List<Action>>h;private Dictionary<string,List<Action<int,string[]>>>l;private List<EasyInterval>p;private List<EasyInterval>q;private List<EasyEvent>r;private EasyCommands u;public virtual void onRunThrottled(float intervalTranspiredPercentage){}public virtual void onTickStart(){}public virtual void onTickComplete(){}public virtual bool onSingleTap(){return false;}public virtual bool onDoubleTap(){return false;}private int v=0;public EasyBlocks Blocks;public const long Microseconds=10;public const long Milliseconds=1000*Microseconds;public const long Seconds=1000*Milliseconds;public const long Minutes=60*Seconds;public const long Hours=60*Minutes;public const long Days=24*Hours;public const long Years=365*Days;public EasyAPI(IMyGridTerminalSystem grid,IMyProgrammableBlock me,Action<string>echo,TimeSpan elapsedTime,string commandArgument="EasyCommand"){this.f=this.d=DateTime.Now.Ticks;this.g=0;this.GridTerminalSystem=EasyAPI.grid=grid;this.Echo=echo;this.ElapsedTime=elapsedTime;this.h=new Dictionary<string,List<Action>>();this.l=new Dictionary<string,List<Action<int,string[]>>>();this.r=new List<EasyEvent>();this.p=new List<EasyInterval>();this.q=new List<EasyInterval>();this.u=new EasyCommands(this);this.Self=new EasyBlock(me);this.Reset();}private void w(){for(int n=0;n<r.Count;n++){if(!(r[n].handle)()){r.Remove(r[n]);}}}public void AddEvent(EasyEvent e){r.Add(e);}public void AddEvent(EasyBlock block,Func<EasyBlock,bool>evnt,Func<EasyBlock,bool>action){this.AddEvent(new EasyEvent(block,evnt,action));}public void AddEvents(EasyBlocks blocks,Func<EasyBlock,bool>evnt,Func<EasyBlock,bool>action){for(int i=0;i<blocks.Count();i++){this.AddEvent(new EasyEvent(blocks.GetBlock(i),evnt,action));}}public List<EasyMessage>GetMessages(){var mymessages=new List<EasyMessage>();var parts=this.Self.Name().Split('\0');if(parts.Length>1){for(int n=1;n<parts.Length;n++){EasyMessage m=new EasyMessage(parts[n]);mymessages.Add(m);}this.Self.SetName(parts[0]);}return mymessages;}public void ClearMessages(){var parts=this.Self.Name().Split('\0');if(parts.Length>1){this.Self.SetName(parts[0]);}}public EasyMessage ComposeMessage(String Subject,String Message){return new EasyMessage(this.Self,Subject,Message);}public void Tick(long interval=0,string argument=""){if(argument!=""){if(this.h.ContainsKey(argument)){for(int n=0;n<this.h[argument].Count;n++){this.h[argument][n]();}}else if(this.l.Count>0){int argc=0;var matches=System.Text.RegularExpressions.Regex.Matches(argument,@"(?<match>[^\s""]+)|""(?<match>[^""]*)""");string[]argv=new string[matches.Count];for(int n=0;n<matches.Count;n++){argv[n]=matches[n].Groups["match"].Value;}argc=argv.Length;if(argc>0&&this.l.ContainsKey(argv[0])){for(int n=0;n<this.l[argv[0]].Count;n++){this.l[argv[0]][n](argc,argv);}}}else if(argument.Substring(0,12)=="EasyCommand "){this.u.handle(argument.Substring(12));}}long now=DateTime.Now.Ticks;if(this.f>this.d&&now-this.f<interval){v++;float transpiredPercentage=((float)((double)(now-this.f)/interval));onRunThrottled(transpiredPercentage);return;}if(v==1){if(onSingleTap()){return;}}else if(v>1){if(onDoubleTap()){return;}}v=0;onTickStart();long lastClock=this.f;this.f=now;this.g=this.f-lastClock;w();for(int n=0;n<this.q.Count;n++){if(this.f>=this.q[n].time){long time=this.f+this.q[n].interval-(this.f-this.q[n].time);(this.q[n].action)();this.q[n]=new EasyInterval(time,this.q[n].interval,this.q[n].action);}}for(int n=0;n<this.p.Count;n++){if(this.f>=this.p[n].time){(this.p[n].action)();p.Remove(this.p[n]);}}onTickComplete();}public long GetDelta(){return this.g;}public long GetClock(){return f;}public void On(string argument,Action callback){if(!this.h.ContainsKey(argument)){this.h.Add(argument,new List<Action>());}this.h[argument].Add(callback);}public void OnCommand(string argument,Action<int,string[]>callback){if(!this.l.ContainsKey(argument)){this.l.Add(argument,new List<Action<int,string[]>>());}this.l[argument].Add(callback);}public void At(long time,Action callback){long t=this.d+time;p.Add(new EasyInterval(t,0,callback));}public void Every(long time,Action callback){q.Add(new EasyInterval(this.f+time,time,callback));}public void In(long time,Action callback){this.At(this.f-this.d+time,callback);}public void Reset(){this.d=this.f;this.ClearMessages();this.Refresh();}public void Refresh(){List<IMyTerminalBlock>kBlocks=new List<IMyTerminalBlock>();GridTerminalSystem.GetBlocks(kBlocks);Blocks=new EasyBlocks(kBlocks);}}public class EasyBlocks{private List<EasyBlock>d;public EasyBlocks(List<IMyTerminalBlock>TBlocks){this.d=new List<EasyBlock>();for(int i=0;i<TBlocks.Count;i++){EasyBlock Block=new EasyBlock(TBlocks[i]);this.d.Add(Block);}}public EasyBlocks(List<EasyBlock>Blocks){this.d=Blocks;}public EasyBlocks(){this.d=new List<EasyBlock>();}public int Count(){return this.d.Count;}public EasyBlock GetBlock(int i){return this.d[i];}public EasyBlocks WithInterface<T>()where T:class{List<EasyBlock>FilteredList=new List<EasyBlock>();for(int i=0;i<this.d.Count;i++){T block=this.d[i].Block as T;if(block!=null){FilteredList.Add(this.d[i]);}}return new EasyBlocks(FilteredList);}public EasyBlocks OfType(String Type){return f("==",Type);}public EasyBlocks NotOfType(String Type){return f("!=",Type);}public EasyBlocks OfTypeLike(String Type){return f("~",Type);}public EasyBlocks NotOfTypeLike(String Type){return f("!~",Type);}public EasyBlocks OfTypeRegex(String Pattern){return f("R",Pattern);}public EasyBlocks NotOfTypeRegex(String Pattern){return f("!R",Pattern);}protected EasyBlocks f(String op,String Type){List<EasyBlock>FilteredList=new List<EasyBlock>();for(int i=0;i<this.d.Count;i++){if(h(op,this.d[i].Type(),Type)){FilteredList.Add(this.d[i]);}}return new EasyBlocks(FilteredList);}public EasyBlocks Named(String Name){return g("==",Name);}public EasyBlocks NotNamed(String Name){return g("!=",Name);}public EasyBlocks NamedLike(String Name){return g("~",Name);}public EasyBlocks NotNamedLike(String Name){return g("!~",Name);}public EasyBlocks NamedRegex(String Pattern){return g("R",Pattern);}public EasyBlocks NotNamedRegex(String Pattern){return g("!R",Pattern);}protected EasyBlocks g(String op,String Name){List<EasyBlock>FilteredList=new List<EasyBlock>();for(int i=0;i<this.d.Count;i++){if(h(op,this.d[i].Name(),Name)){FilteredList.Add(this.d[i]);}}return new EasyBlocks(FilteredList);}public EasyBlocks InGroupsNamed(String Group){return GroupFilter("==",Group);}public EasyBlocks InGroupsNotNamed(String Group){return GroupFilter("!=",Group);}public EasyBlocks InGroupsNamedLike(String Group){return GroupFilter("~",Group);}public EasyBlocks InGroupsNotNamedLike(String Group){return GroupFilter("!~",Group);}public EasyBlocks InGroupsNamedRegex(String Pattern){return GroupFilter("R",Pattern);}public EasyBlocks InGroupsNotNamedRegex(String Pattern){return GroupFilter("!R",Pattern);}public EasyBlocks GroupFilter(String op,String Group){List<EasyBlock>FilteredList=new List<EasyBlock>();List<IMyBlockGroup>groups=new List<IMyBlockGroup>();EasyAPI.grid.GetBlockGroups(groups);List<IMyBlockGroup>matchedGroups=new List<IMyBlockGroup>();for(int n=0;n<groups.Count;n++){if(h(op,groups[n].Name,Group)){matchedGroups.Add(groups[n]);}}for(int n=0;n<matchedGroups.Count;n++){for(int i=0;i<this.d.Count;i++){IMyTerminalBlock block=this.d[i].Block;for(int j=0;j<matchedGroups[n].Blocks.Count;j++){if(block==matchedGroups[n].Blocks[j]){FilteredList.Add(this.d[i]);}}}}return new EasyBlocks(FilteredList);}public EasyBlocks SensorsActive(bool isActive=true){List<EasyBlock>FilteredList=new List<EasyBlock>();for(int i=0;i<this.d.Count;i++){if(this.d[i].Type()=="Sensor"&&((IMySensorBlock)this.d[i].Block).IsActive==isActive){FilteredList.Add(this.d[i]);}}return new EasyBlocks(FilteredList);}public EasyBlocks RoomPressure(String op,Single percent){List<EasyBlock>FilteredList=new List<EasyBlock>();for(int i=0;i<this.d.Count;i++){if(this.d[i].RoomPressure(op,percent)){FilteredList.Add(this.d[i]);}}return new EasyBlocks(FilteredList);}public EasyBlocks FilterBy(Func<EasyBlock,bool>action){List<EasyBlock>FilteredList=new List<EasyBlock>();for(int i=0;i<this.d.Count;i++){if(action(this.d[i])){FilteredList.Add(this.d[i]);}}return new EasyBlocks(FilteredList);}public EasyBlocks First(){List<EasyBlock>FilteredList=new List<EasyBlock>();if(this.d.Count>0){FilteredList.Add(d[0]);}return new EasyBlocks(FilteredList);}public EasyBlocks Add(EasyBlock Block){this.d.Add(Block);return this;}public EasyBlocks Plus(EasyBlocks Blocks){List<EasyBlock>FilteredList=new List<EasyBlock>();FilteredList.AddRange(this.d);for(int i=0;i<Blocks.Count();i++){if(!FilteredList.Contains(Blocks.GetBlock(i))){FilteredList.Add(Blocks.GetBlock(i));}}return new EasyBlocks(FilteredList);}public EasyBlocks Minus(EasyBlocks Blocks){List<EasyBlock>FilteredList=new List<EasyBlock>();FilteredList.AddRange(this.d);for(int i=0;i<Blocks.Count();i++){FilteredList.Remove(Blocks.GetBlock(i));}return new EasyBlocks(FilteredList);}public static EasyBlocks operator+(EasyBlocks a,EasyBlocks b){return a.Plus(b);}public static EasyBlocks operator-(EasyBlocks a,EasyBlocks b){return a.Minus(b);}public EasyBlocks FindOrFail(string message){if(this.Count()==0)throw new Exception(message);return this;}public EasyBlocks Run(EasyAPI api,string type="public"){for(int i=0;i<this.d.Count;i++){this.d[i].Run(api,type);}return this;}public EasyBlocks SendMessage(EasyMessage message){for(int i=0;i<this.d.Count;i++){this.d[i].SendMessage(message);}return this;}public EasyBlocks ApplyAction(String Name){for(int i=0;i<this.d.Count;i++){this.d[i].ApplyAction(Name);}return this;}public EasyBlocks SetProperty<T>(String PropertyId,T value,int bleh=0){for(int i=0;i<this.d.Count;i++){this.d[i].SetProperty<T>(PropertyId,value);}return this;}public T GetProperty<T>(String PropertyId,int bleh=0){return this.d[0].GetProperty<T>(PropertyId);}public EasyBlocks On(){for(int i=0;i<this.d.Count;i++){this.d[i].On();}return this;}public EasyBlocks Off(){for(int i=0;i<this.d.Count;i++){this.d[i].Off();}return this;}public EasyBlocks Toggle(){for(int i=0;i<this.d.Count;i++){this.d[i].Toggle();}return this;}public EasyBlocks RunPB(string argument=""){for(int i=0;i<this.d.Count;i++){this.d[i].RunPB(argument);}return this;}public EasyBlocks WritePublicText(string text){for(int i=0;i<this.d.Count;i++){this.d[i].WritePublicText(text);}return this;}public EasyBlocks WritePrivateText(string text){for(int i=0;i<this.d.Count;i++){this.d[i].WritePublicText(text);}return this;}public EasyBlocks WritePublicTitle(string text){for(int i=0;i<this.d.Count;i++){this.d[i].WritePublicTitle(text);}return this;}public EasyBlocks WritePrivateTitle(string text){for(int i=0;i<this.d.Count;i++){this.d[i].WritePublicTitle(text);}return this;}public EasyBlocks AppendPublicText(string text){for(int i=0;i<this.d.Count;i++){this.d[i].AppendPublicText(text);}return this;}public EasyBlocks AppendPrivateText(string text){for(int i=0;i<this.d.Count;i++){this.d[i].AppendPrivateText(text);}return this;}public EasyInventory Items(){return new EasyInventory(this.d);}public string DebugDump(bool throwIt=true){String output="\n";for(int i=0;i<this.d.Count;i++){output+=this.d[i].Type()+": "+this.d[i].Name()+"\n";}if(throwIt)throw new Exception(output);else return output;}public string DebugDumpActions(bool throwIt=true){String output="\n";for(int i=0;i<this.d.Count;i++){output+="[ "+this.d[i].Type()+": "+this.d[i].Name()+" ]\n";output+="*** ACTIONS ***\n";List<ITerminalAction>actions=this.d[i].GetActions();for(int j=0;j<actions.Count;j++){output+=actions[j].Id+":"+actions[j].Name+"\n";}output+="\n\n";}if(throwIt)throw new Exception(output);else return output;}public string DebugDumpProperties(bool throwIt=true){String output="\n";for(int i=0;i<this.d.Count;i++){output+="[ "+this.d[i].Type()+": "+this.d[i].Name()+" ]\n";output+="*** PROPERTIES ***\n";List<ITerminalProperty>properties=this.d[i].GetProperties();for(int j=0;j<properties.Count;j++){output+=properties[j].TypeName+": "+properties[j].Id+"\n";}output+="\n\n";}if(throwIt)throw new Exception(output);else return output;}private bool h(String op,String a,String b){switch(op){case "==":return(a==b);case "!=":return(a!=b);case "~":return a.Contains(b);case "!~":return!a.Contains(b);case "R":System.Text.RegularExpressions.Match m=(new System.Text.RegularExpressions.Regex(b)).Match(a);while(m.Success){return true;}return false;case "!R":return!h("R",a,b);}return false;}}public struct EasyBlock{public IMyTerminalBlock Block;private IMySlimBlock d;public EasyBlock(IMyTerminalBlock Block){this.Block=Block;this.d=null;}public IMySlimBlock Slim(){if(this.d==null){this.d=this.Block.CubeGrid.GetCubeBlock(this.Block.Position);}return this.d;}public String Type(){return this.Block.DefinitionDisplayNameText;}public Single Damage(){return this.CurrentDamage()/this.MaxIntegrity()*(Single)100.0;}public Single CurrentDamage(){return this.Slim().CurrentDamage;}public Single MaxIntegrity(){return this.Slim().MaxIntegrity;}public bool Open(){IMyDoor door=Block as IMyDoor;if(door!=null){return door.Open;}return false;}public String Name(){return this.Block.CustomName;}public void SendMessage(EasyMessage message){if(Type()=="Programmable block"){SetName(Name()+"\0"+message.Serialize());}}public List<String>NameParameters(char start='[',char end=']'){List<String>matches;this.NameRegex(@"\"+start+@"(.*?)\"+end,out matches);return matches;}public bool RoomPressure(String op,Single percent){String roomPressure=DetailedInfo()["Room pressure"];Single pressure=0;if(roomPressure!="Not pressurized"){pressure=Convert.ToSingle(roomPressure.TrimEnd('%'));}switch(op){case "<":return pressure<percent;case "<=":return pressure<=percent;case ">=":return pressure>=percent;case ">":return pressure>percent;case "==":return pressure==percent;case "!=":return pressure!=percent;}return false;}public Dictionary<String,String>DetailedInfo(){Dictionary<String,String>properties=new Dictionary<String,String>();var statements=this.Block.DetailedInfo.Split('\n');for(int n=0;n<statements.Length;n++){var pair=statements[n].Split(':');properties.Add(pair[0],pair[1].Substring(1));}return properties;}public bool NameRegex(String Pattern,out List<String>Matches){System.Text.RegularExpressions.Match m=(new System.Text.RegularExpressions.Regex(Pattern)).Match(this.Block.CustomName);Matches=new List<String>();bool success=false;while(m.Success){if(m.Groups.Count>1){Matches.Add(m.Groups[1].Value);}success=true;m=m.NextMatch();}return success;}public bool NameRegex(String Pattern){List<String>matches;return this.NameRegex(Pattern,out matches);}public ITerminalAction GetAction(String Name){return this.Block.GetActionWithName(Name);}public EasyBlock ApplyAction(String Name){ITerminalAction Action=this.GetAction(Name);if(Action!=null){Action.Apply(this.Block);}return this;}public T GetProperty<T>(String PropertyId){return Sandbox.ModAPI.Interfaces.TerminalPropertyExtensions.GetValue<T>(this.Block,PropertyId);}public EasyBlock SetProperty<T>(String PropertyId,T value){try{var prop=this.GetProperty<T>(PropertyId);Sandbox.ModAPI.Interfaces.TerminalPropertyExtensions.SetValue<T>(this.Block,PropertyId,value);}catch(Exception e){}return this;}public EasyBlock On(){this.ApplyAction("OnOff_On");return this;}public EasyBlock Off(){this.ApplyAction("OnOff_Off");return this;}public EasyBlock Toggle(){if(this.Block.IsWorking){this.Off();}else{this.On();}return this;}public EasyBlock Run(EasyAPI api,string type="public"){var cmd=new EasyCommands(api);switch(type){case "private":cmd.handle(this.GetPrivateText());break;default:cmd.handle(this.GetPublicText());break;}return this;}public EasyBlock RunPB(string argument=""){IMyProgrammableBlock pb=Block as IMyProgrammableBlock;if(pb!=null){pb.TryRun(argument);}return this;}public string GetPublicText(){string ret="";IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){ret=textPanel.GetPublicText();}return ret;}public string GetPrivateText(){string ret="";IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){ret=textPanel.GetPrivateText();}return ret;}public EasyBlock WritePublicTitle(string text){IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){textPanel.WritePublicTitle(text);}return this;}public EasyBlock WritePrivateTitle(string text){IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){textPanel.WritePrivateTitle(text,false);}return this;}public EasyBlock WritePublicText(string text){IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){textPanel.WritePublicText(text,false);}return this;}public EasyBlock WritePrivateText(string text){IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){textPanel.WritePrivateText(text,false);}return this;}public EasyBlock AppendPublicText(string text){IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){textPanel.WritePublicText(text,true);}return this;}public EasyBlock AppendPrivateText(string text){IMyTextPanel textPanel=Block as IMyTextPanel;if(textPanel!=null){textPanel.WritePrivateText(text,true);}return this;}public EasyBlock SetName(String Name){this.Block.SetCustomName(Name);return this;}public List<ITerminalAction>GetActions(){List<ITerminalAction>actions=new List<ITerminalAction>();this.Block.GetActions(actions);return actions;}public List<ITerminalProperty>GetProperties(){List<ITerminalProperty>properties=new List<ITerminalProperty>();this.Block.GetProperties(properties);return properties;}public EasyInventory Items(Nullable<int>fix_duplicate_name_bug=null){List<EasyBlock>Blocks=new List<EasyBlock>();Blocks.Add(this);return new EasyInventory(Blocks);}public static bool operator==(EasyBlock a,EasyBlock b){return a.Block==b.Block;}public override bool Equals(object o){return(EasyBlock)o==this;}public override int GetHashCode(){return Block.GetHashCode();}public static bool operator!=(EasyBlock a,EasyBlock b){return a.Block!=b.Block;}}public class EasyInventory{public List<EasyItem>Items;public EasyInventory(List<EasyBlock>Blocks){this.Items=new List<EasyItem>();for(int i=0;i<Blocks.Count;i++){EasyBlock Block=Blocks[i];for(int j=0;j<Block.Block.GetInventoryCount();j++){VRage.ModAPI.Ingame.IMyInventory Inventory=Block.Block.GetInventory(j);List<VRage.ModAPI.Ingame.IMyInventoryItem>Items=Inventory.GetItems();for(int k=0;k<Items.Count;k++){this.Items.Add(new EasyItem(Block,j,Inventory,k,Items[k]));}}}}public EasyInventory(List<EasyItem>Items){this.Items=Items;}public EasyInventory OfType(String SubTypeId){List<EasyItem>FilteredItems=new List<EasyItem>();for(int i=0;i<this.Items.Count;i++){if(this.Items[i].Type()==SubTypeId){FilteredItems.Add(this.Items[i]);}}return new EasyInventory(FilteredItems);}public EasyInventory InInventory(int Index){List<EasyItem>FilteredItems=new List<EasyItem>();for(int i=0;i<this.Items.Count;i++){if(this.Items[i].InventoryIndex==Index){FilteredItems.Add(this.Items[i]);}}return new EasyInventory(FilteredItems);}public VRage.MyFixedPoint Count(){VRage.MyFixedPoint Total=0;for(int i=0;i<Items.Count;i++){Total+=Items[i].Amount();}return Total;}public EasyInventory First(){List<EasyItem>FilteredItems=new List<EasyItem>();if(this.Items.Count>0){FilteredItems.Add(this.Items[0]);}return new EasyInventory(FilteredItems);}public void MoveTo(EasyBlocks Blocks,int Inventory=0){for(int i=0;i<Items.Count;i++){Items[i].MoveTo(Blocks,Inventory);}}}public struct EasyItem{private EasyBlock d;public int InventoryIndex;private VRage.ModAPI.Ingame.IMyInventory f;public int ItemIndex;private VRage.ModAPI.Ingame.IMyInventoryItem g;public EasyItem(EasyBlock Block,int InventoryIndex,VRage.ModAPI.Ingame.IMyInventory Inventory,int ItemIndex,VRage.ModAPI.Ingame.IMyInventoryItem Item){this.d=Block;this.InventoryIndex=InventoryIndex;this.f=Inventory;this.ItemIndex=ItemIndex;this.g=Item;}public String Type(int dummy=0){return this.g.Content.SubtypeName;}public VRage.MyFixedPoint Amount(){return this.g.Amount;}public void MoveTo(EasyBlocks Blocks,int Inventory=0,int dummy=0){for(int i=0;i<Blocks.Count();i++){this.f.TransferItemTo(Blocks.GetBlock(i).Block.GetInventory(Inventory),ItemIndex);}}}public struct EasyInterval{public long interval;public long time;public Action action;public EasyInterval(long t,long i,Action a){this.time=t;this.interval=i;this.action=a;}}public struct EasyMessage{public EasyBlock From;public String Subject;public String Message;public long Timestamp;public EasyMessage(String serialized){var parts=serialized.Split(':');if(parts.Length<4){throw new Exception("Error unserializing message.");}int numberInGrid=Convert.ToInt32(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(parts[0])));var blocks=new List<IMyTerminalBlock>();EasyAPI.grid.GetBlocksOfType<IMyProgrammableBlock>(blocks,delegate(IMyTerminalBlock block){return(block as IMyProgrammableBlock).NumberInGrid==numberInGrid;});if(blocks.Count==0){throw new Exception("Message sender no longer exits!");}this.From=new EasyBlock((IMyTerminalBlock)blocks[0]);this.Subject=System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(parts[1]));this.Message=System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(parts[2]));this.Timestamp=Convert.ToInt64(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(parts[3])));}public EasyMessage(EasyBlock From,String Subject,String Message){this.From=From;this.Subject=Subject;this.Message=Message;this.Timestamp=DateTime.Now.Ticks;}public String Serialize(){String text="";text+=System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(""+From.Block.NumberInGrid));text+=":"+System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Subject));text+=":"+System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Message));text+=":"+System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(""+Timestamp));return text;}}public class EasyCommands{private EasyAPI d;private EasyBlocks f;private int g=0;private string h="";public EasyCommands(EasyAPI api){this.d=api;}public void handle(string text){this.g=0;this.h=text;while(g<text.Length){w();g++;}}private void l(string message){throw new Exception("EasyCommand Error: "+message);}private bool p(char c){switch((int)c){case 9:case 10:case 11:case 12:case 13:case 32:return true;}return false;}private bool q(char c){if((48<=c&&c<=57)||(97<=c&&c<=122)||(65<=c&&c<=90))return true;else return false;}private void r(){while(g<h.Length&&p(h[g])){g++;}}private string u(){string identifier="";while(g<h.Length&&q(h[g])){identifier+=h[g];g++;}return identifier;}private string v(){string param="";r();if(g<h.Length&&h[g]=='('){g++;r();if(g<h.Length&&h[g]=='"'){g++;while(g<h.Length){if(h[g]=='"'){g++;break;}else if(h[g]=='\\'&&g+1<h.Length){if(h[g+1]=='"'){param+='"';g++;}else if(h[g+1]=='n'){param+="\n";g++;}else{param+=h[g];}}else{param+=h[g];}g++;}}r();if(g<h.Length&&h[g]==')'){g++;}}return param;}private void w(){while(g<h.Length&&h[g]!=';'){r();string command=u();string parm="";EasyBlocks blks;EasyCommands cmd;switch(command){case "Echo":parm=v();d.Echo(parm);break;case "Blocks":d.Refresh();this.f=d.Blocks;break;case "ApplyAction":parm=v();f.ApplyAction(parm);break;case "WritePublicText":parm=v();f.WritePublicText(parm);break;case "WritePrivateText":parm=v();f.WritePrivateText(parm);break;case "AppendPublicText":parm=v();f.AppendPublicText(parm);break;case "AppendPrivateText":parm=v();f.AppendPrivateText(parm);break;case "On":parm=v();f.On();break;case "Off":parm=v();f.Off();break;case "Toggle":parm=v();f.Toggle();break;case "DebugDump":parm=v();f.DebugDump();break;case "DebugDumpActions":parm=v();f.DebugDumpActions();break;case "DebugDumpProperties":parm=v();f.DebugDumpActions();break;case "Run":parm=v();f.Run(d,parm);break;case "RunPB":parm=v();f.RunPB(parm);break;case "Named":parm=v();f=f.Named(parm);break;case "NamedLike":parm=v();f=f.NamedLike(parm);break;case "NotNamed":parm=v();f=f.NotNamed(parm);break;case "NotNamedLike":parm=v();f=f.NotNamedLike(parm);break;case "InGroupsNamed":parm=v();f=f.InGroupsNamed(parm);break;case "InGroupsNamedLike":parm=v();f=f.InGroupsNamedLike(parm);break;case "InGroupsNotNamed":parm=v();f=f.InGroupsNotNamed(parm);break;case "InGroupsNotNamedLike":parm=v();f=f.InGroupsNotNamedLike(parm);break;case "OfType":parm=v();f=f.OfType(parm);break;case "OfTypeLike":parm=v();f=f.OfTypeLike(parm);break;case "NotOfType":parm=v();f=f.NotOfType(parm);break;case "NotOfTypeLike":parm=v();f=f.OfTypeLike(parm);break;case "":break;default:l("Invalid command: '"+command+"'");break;}r();if(g<h.Length&&h[g]=='.')g++;}}}public class EasyEvent{private Func<EasyBlock,bool>d;private EasyBlock f;private Func<EasyBlock,bool>g;public EasyEvent(EasyBlock obj,Func<EasyBlock,bool>op,Func<EasyBlock,bool>callback){this.d=op;this.g=callback;this.f=obj;}public bool handle(){if((this.d)((EasyBlock)this.f)){return(this.g)((EasyBlock)this.f);}return true;}}public class EasyUtils{public const int LOG_MAX_ECHO_LENGTH_CHARS=8000;public const int LOG_MAX_LCD_LENGTH_CHARS=4200;public static StringBuilder LogBuffer;public static void Log(string logMessage,Action<string>echo=null,IMyProgrammableBlock me=null,string label=null,IMyTextPanel mirrorLcd=null,bool truncateForLcd=true){String output="";if(echo==null){output="\n";output+=logMessage;throw new Exception(output);}if(LogBuffer==null){LogBuffer=new StringBuilder();}if(label!=null){logMessage=label+": "+logMessage;}if(mirrorLcd!=null){string currentlyMirrored=mirrorLcd.GetPublicText();if(truncateForLcd&&LogBuffer.Length+logMessage.Length>LOG_MAX_LCD_LENGTH_CHARS){StringBuilder lcdBuffer=new StringBuilder(LogBuffer.ToString());int charAmountToOffset=fullLineCharsExceeding(lcdBuffer,logMessage.Length,LogBuffer.Length-(LOG_MAX_LCD_LENGTH_CHARS-logMessage.Length));lcdBuffer.Remove(0,LogBuffer.Length-LOG_MAX_LCD_LENGTH_CHARS+charAmountToOffset-2);lcdBuffer.AppendLine();lcdBuffer.Append(logMessage);mirrorLcd.WritePublicText(lcdBuffer.ToString(),false);}else{string potentialNewLine=(currentlyMirrored.Length>0)?"\n":"";mirrorLcd.WritePublicText(potentialNewLine+logMessage,true);}}if(LogBuffer.Length+logMessage.Length*2>LOG_MAX_ECHO_LENGTH_CHARS){int charAmountToRemove=fullLineCharsExceeding(LogBuffer,logMessage.Length);LogBuffer.Remove(0,charAmountToRemove);LogBuffer.Append(output);}if(LogBuffer.Length>0){LogBuffer.AppendLine();}LogBuffer.Append(logMessage);echo(LogBuffer.ToString());}public static int fullLineCharsExceeding(StringBuilder sb,int maxLength,int offset=0){int runningCount=0;for(int i=offset;i<sb.Length;i++){runningCount++;if(sb[i]=='\n'){if(runningCount>maxLength){break;}}}return runningCount;}public static void ClearLogBuffer(){LogBuffer.Clear();}public static double Max(double[]values){double runningMax=values[0];for(int i=1;i<values.Length;i++){runningMax=Math.Max(runningMax,values[i]);}return runningMax;}public static double Min(double[]values){double runningMin=values[0];for(int i=1;i<values.Length;i++){runningMin=Math.Min(runningMin,values[i]);}return runningMin;}}public class EasyLCD{public char[]buffer;IMyTextPanel d;EasyBlock f;public int width;public int height;public int xDisplays=0;public int yDisplays=0;private int g=36;private int h=18;Single l;public EasyLCD(EasyBlocks block,double scale=1.0){this.f=block.GetBlock(0);if(this.f.Type()=="Wide LCD panel")this.g=72;this.d=(IMyTextPanel)(block.GetBlock(0).Block);this.l=block.GetProperty<Single>("FontSize");this.width=(int)((double)this.g/this.l);this.height=(int)((double)this.h/this.l);this.buffer=new char[this.width*this.height];this.clear();this.update();}public void SetText(String text,bool append=false){this.d.WritePublicText(text,append);}public void plot(EasyBlocks blocks,double x,double y,double scale=1.0,char brush='o',bool showBounds=true,char boundingBrush='?'){VRageMath.Vector3D max=new Vector3D(this.d.CubeGrid.Max);VRageMath.Vector3D min=new Vector3D(this.d.CubeGrid.Min);VRageMath.Vector3D size=new Vector3D(max-min);int width=(int)size.GetDim(0);int height=(int)size.GetDim(1);int depth=(int)size.GetDim(2);int minX=(int)min.GetDim(0);int minY=(int)min.GetDim(1);int minZ=(int)min.GetDim(2);int maxX=(int)max.GetDim(0);int maxY=(int)max.GetDim(1);int maxZ=(int)max.GetDim(2);double s=(double)depth+0.01;if(width>depth){s=(double)width+0.01;}if(showBounds){box(x+-(((0-(width/2.0))/s)*scale),y+-(((0-(depth/2.0))/s)*scale),x+-(((maxX-minX-(width/2.0))/s)*scale),y+-(((maxZ-minZ-(depth/2.0))/s)*scale),boundingBrush);}for(int n=0;n<blocks.Count();n++){var block=blocks.GetBlock(n);Vector3D pos=new Vector3D(block.Block.Position);pset(x+-((((double)(pos.GetDim(0)-minX-(width/2.0))/s))*scale),y+-((((double)(pos.GetDim(2)-minZ-(depth/2.0))/s))*scale),brush);}}public void pset(double x,double y,char brush='o'){if(x>=0&&x<1&&y>=0&&y<1){this.buffer[this.q(x,y)]=brush;}}private void p(int x,int y,char brush='0'){if(x>=0&&x<this.width&&y>=0&&y<this.height){this.buffer[x+(y*this.width)]=brush;}}public void text(double x,double y,String text){int xx=(int)(x*(this.width-1));int yy=(int)(y*(this.height-1));for(int xs=0;xs<text.Length;xs++){p(xx+xs,yy,text[xs]);}}public void clear(char brush=' '){for(int n=0;n<this.buffer.Length;n++){this.buffer[n]=brush;}}public void update(){String s="";String space="";for(int y=0;y<this.height;y++){space="";for(int x=0;x<this.width;x++){if(buffer[x+(y*this.width)]==' '){space+="  ";}else{s+=space+buffer[x+(y*this.width)];space="";}}s+="\n";}this.d.WritePublicText(s);}private int q(double x,double y){int xx=(int)(x*(this.width-1));int yy=(int)(y*(this.height-1));return xx+yy*this.width;}public void line(double xx0,double yy0,double xx1,double yy1,char brush='o'){int x0=(int)Math.Floor(xx0*(this.width));int y0=(int)Math.Floor(yy0*(this.height));int x1=(int)Math.Floor(xx1*(this.width));int y1=(int)Math.Floor(yy1*(this.height));bool steep=Math.Abs(y1-y0)>Math.Abs(x1-x0);if(steep){int tmp=x0;x0=y0;y0=tmp;tmp=x1;x1=y1;y1=tmp;}if(x0>x1){int tmp=x0;x0=x1;x1=tmp;tmp=y0;y0=y1;y1=tmp;}int dX=(x1-x0),dY=Math.Abs(y1-y0),err=(dX/2),ystep=(y0<y1?1:-1),y=y0;for(int x=x0;x<=x1;++x){if(steep)p(y,x,brush);else p(x,y,brush);err=err-dY;if(err<0){y+=ystep;err+=dX;}}}public void box(double x0,double y0,double x1,double y1,char brush='o'){line(x0,y0,x1,y0,brush);line(x1,y0,x1,y1,brush);line(x1,y1,x0,y1,brush);line(x0,y1,x0,y0,brush);}}
