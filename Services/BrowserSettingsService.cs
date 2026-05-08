using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TimeCheck.Pwa.Services;

public class BrowserSettingsService : ISettingsService
{
    private const string StorageKey = "timecheck.pwa.settings.v1";
    private const int DefaultsVersion = 1;
    private readonly IJSRuntime _js;

    public bool IsQuiet { get; set; } = false;
    public int TimeCheckIntervalMinutes { get; set; } = 5;
    public int EncouragementIntervalMin { get; set; } = 10;
    public int EncouragementIntervalMax { get; set; } = 20;
    public bool EncouragementEnabled { get; set; } = true;
    public List<string> EncouragementMessages { get; set; } = new List<string>
    {
        "Move it, you lazy sod — pedal like the sergeant's watching!",
        "Put some bleeding effort into it, you horrible little man!",
        "Eyes front, legs turning — show those tarmac traitors who's boss!",
        "Pick up the pace, you dawdling peacock!",
        "Don't wheeze like a pensioner; give it some welly!",
        "You're not on a Sunday stroll — pedal like it's a route march!",
        "Shift that backside and make those pedals pay attention!",
        "Come on, you glorious wreck, churn those gears!",
        "Legs like pistons, soldier — get them firing!",
        "If the sergeant heard that wheeze he'd have you doing laps!",
        "Stop admiring the scenery and start punishing the road  You plank!",
        "Waste not a breath moaning — burn it into forward fricking motion!",
        "Bend metal with your thighs, you maggot!",
        "Don't be a biscuit — pedal like someone stole your wallet!",
        "One more push and you'll be less pathetic and more presentable!",
        "Put some bleeding effort into it",
        "Keep it moving, you daft prat!",
        "Slog through it — the hill's just showing off, not you!",
        "Come on no pain no gain",
        "Don't be a limp BISCUIT; be a proper bit of kit!",
        "Come on do a proper job you little plonker!",
        "Pedal like you put a bet on your finish time!",
        "No dawdling — the road doesn't care about your excuses!",
        "Sound off with your legs, not your complaints!",
        "Put some effort in it at the back",
        "The last one to the caffe pays the bill",
        "Give it the beans, you marvelous underachiever!",
        "Quit moaning and let your wheels do the talking!",
        "Harden up and pedal, — charm is strictly optional!",
        "Stop faffing around and make that incline regret its choices!",
        "Sweat like a saint and pedal like a sinner caught stealing!",
        "Pull yourself together and show that hill no mercy!",
        "Legs on fire? Good — that's improvement cooking!",
        "Mind over MATTER — think hard, pedal harder!",
        "You're nearly there, you stubborn bit of brilliance!",
        "Keep going — this isn't supposed to be easy, darling!",
        "Hustle up, you caffeine-fuelled battalion of one!",
        "If you slow now you'll only have to face the shame later!",
        "Charge like a confused cavalryman — full speed, less thinking!",
        "Move like you mean it, and mean it loudly!",
        "Stop being polite to the hill — it's rude enough already!",
        "This isn't a promenade — it's a proving ground!",
        "Pedal like you've misplaced your dignity and found it downhill!",
        "Be the nuisance the hill never asked for!",
        "Get on with it — the tarmac won't applaud, but you'll know!",
        "Hurry up, you magnificent so-and-so, and keep those legs honest!",
        "Power through like a bloke with a point to prove!",
        "Don't let the hill have the last laugh — pedal louder!",
        "If your legs could speak they'd apologise for the noise. Make them proud!",
        "Stop looking for sympathy — the road gives none!",
        "Give 'em hell and call it an interval session!",
        "Pedal like you owe the crown money and they're coming to collect!",
        "Hurry up — the next village won't wait for your theatrics!",
        "When in doubt, stand on the pedals and swear at the incline!",
        "Put a bit of elbow grease into those pedals, why don't you!",
        "Muster some grit and show that slope who's boss!",
        "Don't be meeker than a mouse in parade rest — push!",
        "Squeeze the road for all it's worth; there's no refund!",
        "Act like it's the last mile of the parade — loud and proud!",
        "Leg power now, excuses at the pub later!",
        "You're nearly earning your bragging rights — don't squander them!",
        "Give it a right old go, you splendidly misdirected soul!",
        "If you can grumble, you can pedal harder — start doing both!",
        "Pretend you're late for tea — nothing gets you going like that!",
        "Push like a corporal with a stopwatch — efficient and noisy!",
        "Stop being delicate; be a proper, slightly sweaty legend!",
        "Drive those pedals like they're enemy territory!",
        "Don't just roll — dominate the rotation!",
        "Look fierce, pedal fiercer — psychological warfare, that is!",
        "If your legs had medals, they'd be heavy by now — earn 'em!",
        "Don't give the hill satisfaction — take it for yourself!",
        "Act like you trained for this in a shed and keep proving it!",
        "Throw some oomph into it — your bike needs moral support!",
        "Keep the cadence up; lethargy is for someone else's ride!",
        "Stride those pedals with the stubbornness of a mule and the grace of a drunk dancer!",
        "Put the boot in, metaphorically and with your calves!",
        "Imagine the hill's your ex — pass it without apology!",
        "Don't ask for easy; ask for more pedals and less complaining!",
        "Act like this is training for something mysterious and important!",
        "You're not here to look pretty, you're here to get up the hill!",
        "Stand up, push down, and swear softly at your inner critic!",
        "Remember: sweat is just proof you've been brutally honest with yourself!",
        "Be the sort of cyclist that makes the hill reconsider its life choices!",
        "Push like a man who knows the pub shutters close soon!",
        "Pedal like you nicked somebody's sandwich and need to get away!",
        "Keep going — half-hearted effort is for vegetables!",
        "Treat the hill like a minor annoyance and ride it out!",
        "You look better in motion; keep the show on the road!",
        "Grin like a soldier, pedal like a machine — results follow!",
        "Pedal with intent or at least with good posture!",
        "Show that gradient you have a spine of iron and a sense of humour!",
        "Don't be a spectator in your own ride — be the event!",
        "Finish this climb and call it a character-building exercise!",
        "Legs, meet challenge. Challenge, meet relentless persistence!",
        "When your legs scream, that's just applause from the future you!",
        "Be ridiculous, be brave, be sweaty — and keep pedalling!",
        "You've got the kit and the cheek — now use both!",
        "Make this climb regret ever daring to stand in your way!",
        "Now pedal, you glorious incompetent — make it count!",
        "Keep the chain singing and the sweat flowing — you're in charge of this ride.",
        "This road doesn't know what hit it. Show it some proper cycling !",
        "Look out your back wheel is following your front one",
        "Come on there's only seven more hills",
        "Pretend there's a lion chasing you",
        "Spin those wheels like you're chasing the last bus home.",
        "Be the kind of rider who turns every hill into a punchline.",
        "Always tell your MUMMY before you go off somewhere — especially if it's up a hill!",
        "Ride like your bike has a personality — loud, stubborn, and full of grit.",
        "If you're on an electric bike press boost now otherwise PEDAL"
    };

    private static readonly string[] AdditionalMilitaryEncouragements = new string[]
    {
        "GET YOUR ARSE IN GEAR!",
        "MOVE, MOVE, MOVE!",
        "PEDAL LIKE YOU'VE STOLEN SOMETHING!",
        "SHUT UP AND TURN THOSE CRANKS!",
        "NO WHINING, ONLY WATTAGE!",
        "PUSH THE PEDALS, NOT YOUR LAMENTS!",
        "TURN THE PAIN INTO POWER!",
        "MAKE THE ROAD REGRET MEETING YOU!",
        "GRIND THOSE GEARS, SOLDIER!",
        "STAND UP AND SMASH THE CLIMB!",
        "SPEED UP, YOU BLOODY LEGEND!",
        "STOP SIMMERING, START CHURNING!",
        "BRING THE BEAST MODE!",
        "NO PITY, ONLY PEDALS!",
        "HARDEN UP AND HIT THE GEAR!",
        "BE LOUD WITH YOUR WHEELS!",
        "BITE DOWN, PUSH ON!",
        "MAKE THE HILL APOLOGISE!",
        "IF YOU'RE BREATHING, ACCELERATE!",
        "DON'T CRAWL — CHARGE!",
        "RELOAD YOUR LEGS AND FIRE!",
        "CRUSH THE CADENCE!",
        "THROW OOMPH INTO THE PEDALS!",
        "OUTPACE YOUR EXCUSES!",
        "MAKE EVERY STROKE COUNT!",
        "DROP THE CHARM, FIND THE POWER!",
        "PEDAL LIKE YOU MEAN IT!",
        "SPANK THE GEARS!",
        "HIT THE GAS WITH YOUR CALVES!",
        "MAKE IT HURT — THAT'S PROGRESS!",
        "SWING THE LEGS, NOT THE ARGUMENTS!",
        "TURN WEAKNESS INTO WATTS!",
        "LEAVE LAZINESS IN THE DUST!",
        "SQUEEZE, PRESS, DOMINATE!",
        "YOUR LEGS ARE WEAPONS — USE THEM!",
        "UNLEASH THE AGGRESSION, QUIET THE DOUBT!",
        "PUT SOME BLOOD IN THOSE PEDALS!",
        "DON'T BE GENTLE — BE FEROCIOUS!",
        "STOMP THE ROAD, YOU GLORIOUS LEGEND!",
        "PUSH UNTIL YOUR MIND GIVES IN!",
        "MAKE THE CLOCK APOLOGISE!",
        "FLEX THE MUSCLES, FEED THE SPEED!",
        "SHOW THE HILL NO MERCY!",
        "NO HALF MEASURES — GO FULL THROTTLE!",
        "HIT THE INTERVAL, NOT THE SNOOZE!",
        "PAIN IS TEMPORARY, PRIDE IS FOREVER!",
        "BE THE FORCE THAT MOVES THE ROAD!",
        "BE GREATER THAN YOUR EXCUSES!",
        "DROP THE TALK, START THE TURNS!",
        "SWEAT NOW, BRAG LATER!",
        "MAKE THE SLOPE REGRET EXISTING!",
        "SPEED IS YOUR REPRISAL!",
        "REAP SPEED, SOW EFFORT!",
        "SQUEEZE THE KNEES, FEED THE WHEELS!",
        "SWITCH ON, STOMP OFF!",
        "MAKE IT A BLUR, NOT A SLOG!",
        "GO HARD OR GO HOME!",
        "BRING THE RUCKUS TO THE ROAD!",
        "TURN YOUR GRIMACE INTO SPEED!",
        "CHURN LIKE A MACHINE!",
        "NO SOFTNESS, JUST FORCE!",
        "PUSH, PULL, PROPEL!",
        "SHOW THE HILL WHO OWNS THIS!",
        "STRAIGHTEN THAT BACK, PUSH!",
        "FORWARD, ALWAYS FORWARD!",
        "HURRY UP, YOU MAGNIFICENT IDIOT!",
        "PUT SOME WELLY INTO IT!",
        "GRIP THE BARS, GRIP VICTORY!",
        "MAKE THE ROAD FEEL YOUR WRATH!",
        "THIS IS NO WALK IN THE PARK — SMASH IT!",
        "FILL YOUR LUNGS AND FILL THE GAP!",
        "SWEEP THE PAVEMENT WITH YOUR WHEELS!",
        "IF YOU WANT EASY, GET OFF — WE DON'T!",
        "MAKE EVERY REVOLUTION COUNT!",
        "NO MERCY FOR THE HILL!",
        "TURN COMPLAINTS INTO CADENCE!",
        "SQUASH THE RESISTANCE!",
        "LEG POWER, MENTAL STEEL!",
        "HIT IT LIKE YOU OWE IT!",
        "MAKE IT A MISSION, NOT A MEANDER!",
        "FLOOD THE STRAIGHT WITH SPEED!",
        "LEAD THE ATTACK WITH YOUR CALVES!",
        "MAKE THE GEAR FEEL YOUR INTENT!",
        "SPIT FIRE WITH YOUR PEDALS!",
        "BE BOLD, BE SUDDEN, BE QUICK!",
        "DON'T YOU DARE SLOW DOWN!",
        "CLAIM THE ROAD, ONE TURN AT A TIME!",
        "DON'T THINK, JUST DO!",
        "SWAP EXCUSES FOR EFFORT!",
        "BE LOUD WITH YOUR LEGS, NOT YOUR LIPS!",
        "STAMP THE GROUND WITH WHEEL ROTATION!",
        "PUT THE BOOT IN AND KEEP IT THERE!",
        "GO LIKE IT'S THE LAST LAP!",
        "MAKE YOUR LEGS PROUD!",
        "LEAVE REGRET BEHIND, LEAVE AWAKE SPEED!",
        "CHARGE THE ASCENT, NO RETREAT!",
        "THRASH THE ROTATION!",
        "MAKE THE PATH FEAR YOU!",
        "DRIVE EVERY TURN WITH FURY!",
        "NOW PEDAL, FOR GLORY AND SANITY!"
    };

    public BrowserSettingsService(IJSRuntime js)
    {
        _js = js;
        EncouragementMessages.AddRange(AdditionalMilitaryEncouragements);
    }

    public async ValueTask LoadAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string>("timecheck.storageGet", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                var dto = JsonSerializer.Deserialize<BrowserSettingsDto>(json);
                if (dto != null)
                {
                    IsQuiet = dto.IsQuiet;
                    TimeCheckIntervalMinutes = dto.TimeCheckIntervalMinutes > 0 ? dto.TimeCheckIntervalMinutes : TimeCheckIntervalMinutes;
                    EncouragementIntervalMin = dto.EncouragementIntervalMin > 0 ? dto.EncouragementIntervalMin : EncouragementIntervalMin;
                    EncouragementIntervalMax = dto.EncouragementIntervalMax > 0 ? dto.EncouragementIntervalMax : EncouragementIntervalMax;
                    EncouragementEnabled = dto.EncouragementEnabled;

                    if (dto.EncouragementMessages == null)
                    {
                        // No persisted messages: keep the in-memory defaults (which already include our additions)
                        EncouragementMessages = EncouragementMessages;
                    }
                    else
                    {
                        // Persisted messages exist. If defaults haven't been applied, merge missing defaults once.
                        var persisted = dto.EncouragementMessages;

                        if (dto.DefaultsVersion < DefaultsVersion)
                        {
                            var existing = new HashSet<string>(persisted, StringComparer.OrdinalIgnoreCase);
                            var defaults = new List<string>(EncouragementMessages);
                            var toAdd = defaults.Where(d => !existing.Contains(d)).ToList();
                            if (toAdd.Count > 0)
                            {
                                persisted.AddRange(toAdd);
                                // update dto and save merged settings back to storage
                                try
                                {
                                    dto.EncouragementMessages = persisted;
                                    dto.DefaultsVersion = DefaultsVersion;
                                    var mergedJson = JsonSerializer.Serialize(dto);
                                    await _js.InvokeVoidAsync("timecheck.storageSet", StorageKey, mergedJson);
                                }
                                catch { }
                            }
                        }

                        EncouragementMessages = persisted;
                    }
                }
            }
        }
        catch { }
    }

    public async ValueTask SaveAsync()
    {
        var dto = new BrowserSettingsDto
        {
            IsQuiet = IsQuiet,
            TimeCheckIntervalMinutes = TimeCheckIntervalMinutes,
            EncouragementIntervalMin = EncouragementIntervalMin,
            EncouragementIntervalMax = EncouragementIntervalMax,
            EncouragementEnabled = EncouragementEnabled,
            EncouragementMessages = EncouragementMessages,
            DefaultsVersion = DefaultsVersion
        };

        var json = JsonSerializer.Serialize(dto);
        await _js.InvokeVoidAsync("timecheck.storageSet", StorageKey, json);
    }

    private class BrowserSettingsDto
    {
        public bool IsQuiet { get; set; }
        public int TimeCheckIntervalMinutes { get; set; }
        public int EncouragementIntervalMin { get; set; }
        public int EncouragementIntervalMax { get; set; }
        public bool EncouragementEnabled { get; set; }
        public List<string>? EncouragementMessages { get; set; }
        public int DefaultsVersion { get; set; } = 0;
    }
}
